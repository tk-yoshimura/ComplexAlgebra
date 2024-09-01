using DoubleDouble;
using DoubleDoubleComplex;
using System.Diagnostics;

namespace ComplexAlgebra {
    public partial class ComplexMatrix {
        private static ComplexMatrix Invert2x2(ComplexMatrix m) {
            Debug.Assert(m.Shape == (2, 2));

            int exponent = m.MaxExponent;
            m = ScaleB(m, -exponent);

            Complex det = Det2x2(m);

            return new ComplexMatrix(
                new Complex[,] {
                    { m.e[1, 1], -m.e[0, 1] },
                    { -m.e[1, 0], m.e[0, 0] }
                }, cloning: false
            ) / (ddouble.Ldexp(det.R, exponent), ddouble.Ldexp(det.I, exponent));
        }

        private static ComplexMatrix Invert3x3(ComplexMatrix m) {
            Debug.Assert(m.Shape == (3, 3));

            int exponent = m.MaxExponent;
            m = ScaleB(m, -exponent);

            Complex det = Det3x3(m);
            return new ComplexMatrix(
                new Complex[,] {
                    { m.e[1, 1] * m.e[2, 2] - m.e[1, 2] * m.e[2, 1],
                      m.e[0, 2] * m.e[2, 1] - m.e[0, 1] * m.e[2, 2],
                      m.e[0, 1] * m.e[1, 2] - m.e[0, 2] * m.e[1, 1] },
                    { m.e[1, 2] * m.e[2, 0] - m.e[1, 0] * m.e[2, 2],
                      m.e[0, 0] * m.e[2, 2] - m.e[0, 2] * m.e[2, 0],
                      m.e[0, 2] * m.e[1, 0] - m.e[0, 0] * m.e[1, 2] },
                    { m.e[1, 0] * m.e[2, 1] - m.e[1, 1] * m.e[2, 0],
                      m.e[0, 1] * m.e[2, 0] - m.e[0, 0] * m.e[2, 1],
                      m.e[0, 0] * m.e[1, 1] - m.e[0, 1] * m.e[1, 0] }
                }, cloning: false
            ) / (ddouble.Ldexp(det.R, exponent), ddouble.Ldexp(det.I, exponent));
        }

        public static ComplexMatrix GaussianEliminate(ComplexMatrix m) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;

            if (!IsFinite(m)) {
                return Invalid(n, n);
            }

            int exponent = m.MaxExponent;
            ComplexMatrix v = Identity(m.Rows), u = ScaleB(m, -exponent);

            for (int i = 0; i < n; i++) {
                ddouble pivot_abs = u.e[i, i].Norm;
                int p = i;

                for (int j = i + 1; j < n; j++) {
                    if (u.e[j, i].Norm > pivot_abs) {
                        pivot_abs = u.e[j, i].Norm;
                        p = j;
                    }
                }

                if (ddouble.ILogB(pivot_abs) <= -204) {
                    return Invalid(v.Rows, v.Columns);
                }

                if (p != i) {
                    for (int j = 0; j < n; j++) {
                        (u.e[p, j], u.e[i, j]) = (u.e[i, j], u.e[p, j]);
                    }

                    for (int j = 0; j < v.Columns; j++) {
                        (v.e[p, j], v.e[i, j]) = (v.e[i, j], v.e[p, j]);
                    }
                }

                Complex inv_mii = 1 / u.e[i, i];
                u.e[i, i] = 1;
                for (int j = i + 1; j < n; j++) {
                    u.e[i, j] *= inv_mii;
                }
                for (int j = 0; j < v.Columns; j++) {
                    v.e[i, j] *= inv_mii;
                }

                for (int j = i + 1; j < n; j++) {
                    Complex mul = u.e[j, i];
                    u.e[j, i] = Complex.Zero;
                    for (int k = i + 1; k < n; k++) {
                        u.e[j, k] -= u.e[i, k] * mul;
                    }
                    for (int k = 0; k < v.Columns; k++) {
                        v.e[j, k] -= v.e[i, k] * mul;
                    }
                }
            }

            for (int i = n - 1; i >= 0; i--) {
                for (int j = i - 1; j >= 0; j--) {
                    Complex mul = u.e[j, i];
                    for (int k = i; k < n; k++) {
                        u.e[j, k] = Complex.Zero;
                    }
                    for (int k = 0; k < v.Columns; k++) {
                        v.e[j, k] -= v.e[i, k] * mul;
                    }
                }
            }

            v = ScaleB(v, -exponent);

            return v;
        }

        public static ComplexVector Solve(ComplexMatrix m, ComplexVector v) {
            if (!IsSquare(m) || m.Size != v.Dim) {
                throw new ArgumentException("invalid size", $"{nameof(m)}, {nameof(v)}");
            }

            int n = m.Size;

            if (!IsFinite(m)) {
                return ComplexVector.Invalid(n);
            }

            int exponent = m.MaxExponent;
            ComplexMatrix u = ScaleB(m, -exponent);
            v = v.Copy();

            for (int i = 0; i < n; i++) {
                ddouble pivot_abs = u.e[i, i].Norm;
                int p = i;

                for (int j = i + 1; j < n; j++) {
                    if (u.e[j, i].Norm > pivot_abs) {
                        pivot_abs = u.e[j, i].Norm;
                        p = j;
                    }
                }

                if (ddouble.ILogB(pivot_abs) <= -204) {
                    return ComplexVector.Invalid(v.Dim);
                }

                if (p != i) {
                    for (int j = 0; j < n; j++) {
                        (u.e[p, j], u.e[i, j]) = (u.e[i, j], u.e[p, j]);
                    }

                    (v[p], v[i]) = (v[i], v[p]);
                }

                Complex inv_mii = 1 / u.e[i, i];
                u.e[i, i] = Complex.One;
                for (int j = i + 1; j < n; j++) {
                    u.e[i, j] *= inv_mii;
                }
                v[i] *= inv_mii;

                for (int j = i + 1; j < n; j++) {
                    Complex mul = u.e[j, i];
                    u.e[j, i] = Complex.Zero;
                    for (int k = i + 1; k < n; k++) {
                        u.e[j, k] -= u.e[i, k] * mul;
                    }
                    v[j] -= v[i] * mul;
                }
            }

            for (int i = n - 1; i >= 0; i--) {
                for (int j = i - 1; j >= 0; j--) {
                    Complex mul = u.e[j, i];
                    for (int k = i; k < n; k++) {
                        u.e[j, k] = Complex.Zero;
                    }
                    v[j] -= v[i] * mul;
                }
            }

            v = ComplexVector.ScaleB(v, -exponent);

            return v;
        }
    }
}
