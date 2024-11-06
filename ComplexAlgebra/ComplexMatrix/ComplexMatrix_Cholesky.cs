using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexMatrix {
        public static ComplexMatrix Cholesky(ComplexMatrix m, bool enable_check_hermitian = true) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;

            if ((enable_check_hermitian && !IsHermitian(m)) || !IsFinite(m)) {
                return Invalid(n);
            }

            if (IsZero(m)) {
                return Zero(n);
            }

            int exponent = (m.MaxExponent / 2) * 2;

            ComplexMatrix u = ScaleB(m, -exponent);

            Complex[,] v = new Complex[n, n];

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < i; j++) {
                    Complex v_ij = u[i, j];
                    for (int k = 0; k < j; k++) {
                        v_ij -= v[i, k] * v[j, k].Conj;
                    }
                    v[i, j] = v_ij / v[j, j];
                }

                Complex v_ii = u[i, i];
                for (int k = 0; k < i; k++) {
                    v_ii -= v[i, k] * v[i, k].Conj;
                }
                v[i, i] = Complex.Sqrt(v_ii);

                for (int j = i + 1; j < n; j++) {
                    v[i, j] = Complex.Zero;
                }
            }

            ComplexMatrix l = ScaleB(new ComplexMatrix(v, cloning: false), exponent / 2);

            return l;
        }

        public static ComplexMatrix InversePositiveHermitian(ComplexMatrix m, bool enable_check_hermitian = true) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;

            if (!IsFinite(m)) {
                return Invalid(n, n);
            }

            ComplexMatrix l = Cholesky(m, enable_check_hermitian);

            if (!IsFinite(l)) {
                return Invalid(n);
            }

            ComplexMatrix v = Identity(n);

            for (int i = 0; i < n; i++) {
                Complex inv_mii = 1d / l.e[i, i];
                for (int j = 0; j < n; j++) {
                    v.e[i, j] *= inv_mii;
                }

                for (int j = i + 1; j < n; j++) {
                    Complex mul = l.e[j, i];
                    for (int k = 0; k < n; k++) {
                        v.e[j, k] -= v.e[i, k] * mul;
                    }
                }
            }

            Complex[,] ret = new Complex[n, n];
            v = v.T;

            for (int i = 0; i < n; i++) {
                for (int j = 0; j <= i; j++) {
                    Complex s = 0d;

                    for (int k = i; k < n; k++) {
                        s += v.e[i, k].Conj * v.e[j, k];
                    }

                    if (i != j) {
                        ret[i, j] = s;
                        ret[j, i] = s.Conj;
                    }
                    else {
                        ret[i, i] = s.R;
                    }
                }
            }

            ComplexMatrix w = new(ret, cloning: false);

            return w;
        }

        public static ComplexVector SolvePositiveHermitian(ComplexMatrix m, ComplexVector v, bool enable_check_hermitian = true) {
            if (!IsSquare(m) || m.Size != v.Dim) {
                throw new ArgumentException("invalid size", $"{nameof(m)}, {nameof(v)}");
            }

            int n = m.Size;

            if (!IsFinite(m)) {
                return ComplexVector.Invalid(n);
            }

            ComplexMatrix l = Cholesky(m, enable_check_hermitian);

            if (!IsFinite(l)) {
                return ComplexVector.Invalid(n);
            }

            v = v.Copy();

            for (int i = 0; i < n; i++) {
                Complex inv_mii = 1d / l.e[i, i];
                v[i] *= inv_mii;

                for (int j = i + 1; j < n; j++) {
                    Complex mul = l.e[j, i];
                    v[j] -= v[i] * mul;
                }
            }

            for (int i = n - 1; i >= 0; i--) {
                Complex inv_mii = 1d / l.e[i, i].Conj;
                v[i] *= inv_mii;

                for (int j = i - 1; j >= 0; j--) {
                    Complex mul = l.e[i, j].Conj;
                    v[j] -= v[i] * mul;
                }
            }

            return v;
        }
    }
}
