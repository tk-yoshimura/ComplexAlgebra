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
                        v_ij -= v[i, k] * Complex.Conjugate(v[j, k]);
                    }
                    v[i, j] = v_ij / v[j, j]; 
                }

                Complex v_ii = u[i, i];
                for (int k = 0; k < i; k++) {
                    v_ii -= v[i, k] * Complex.Conjugate(v[i, k]);
                }
                v[i, i] = Complex.Sqrt(v_ii);

                for (int j = i + 1; j < n; j++) {
                    v[i, j] = Complex.Zero;
                }
            }

            ComplexMatrix l = ScaleB(new ComplexMatrix(v, cloning: false), exponent / 2);

            return l;
        }
    }
}
