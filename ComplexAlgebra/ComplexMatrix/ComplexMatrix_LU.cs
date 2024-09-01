using Algebra;
using DoubleDouble;
using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexMatrix {
        private static (int[] pivot, int pivot_det, ComplexMatrix l, ComplexMatrix u) LUKernel(ComplexMatrix m) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;

            int[] ps = (new int[n]).Select((_, idx) => idx).ToArray();
            int pivot_det = 1;

            if (!IsFinite(m)) {
                return (ps, 1, Invalid(n), Invalid(n));
            }
            if (IsZero(m)) {
                return (ps, 1, Invalid(n), Zero(n));
            }

            int exponent = m.MaxExponent;
            m = ScaleB(m, -exponent);

            ComplexMatrix l = Zero(n), u = Zero(n);

            for (int i = 0; i < n; i++) {
                ddouble pivot_abs = m.e[i, i].Norm;
                int r = i;

                for (int j = i + 1; j < n; j++) {
                    if (m.e[j, i].Norm > pivot_abs) {
                        pivot_abs = m.e[j, i].Norm;
                        r = j;
                    }
                }

                if (ddouble.ILogB(pivot_abs) <= -204) {
                    return (ps, 0, Invalid(n), Zero(n));
                }

                if (r != i) {
                    for (int j = 0; j < n; j++) {
                        (m.e[r, j], m.e[i, j]) = (m.e[i, j], m.e[r, j]);
                    }

                    (ps[r], ps[i]) = (ps[i], ps[r]);

                    pivot_det = -pivot_det;
                }

                for (int j = i + 1; j < n; j++) {
                    Complex mul = m.e[j, i] / m.e[i, i];
                    m.e[j, i] = mul;

                    for (int k = i + 1; k < n; k++) {
                        m.e[j, k] -= m.e[i, k] * mul;
                    }
                }
            }

            for (int i = 0; i < n; i++) {
                l.e[i, i] = Complex.One;

                int j = 0;
                for (; j < i; j++) {
                    l.e[i, j] = m.e[i, j];
                }
                for (; j < n; j++) {
                    u.e[i, j] = m.e[i, j];
                }
            }

            u = ScaleB(u, exponent);

            return (ps, pivot_det, l, u);
        }

        public static (Matrix p, ComplexMatrix l, ComplexMatrix u) LU(ComplexMatrix m) {
            (int[] ps, int pivot_det, ComplexMatrix l, ComplexMatrix u) = LUKernel(m);

            int n = m.Size;

            if (pivot_det == 0) {
                return (Matrix.Identity(n), l, u);
            }

            Matrix p = Matrix.Zero(n, n);

            for (int i = 0; i < n; i++) {
                p[i, ps[i]] = ddouble.One;
            }

            return (p, l, u);
        }
    }
}
