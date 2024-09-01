using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexVector {
        public static bool Any(ComplexVector vector, Func<Complex, bool> cond) {
            for (int i = 0; i < vector.Dim; i++) {
                if (cond(vector.v[i])) {
                    return true;
                }
            }

            return false;
        }

        public static bool All(ComplexVector vector, Func<Complex, bool> cond) {
            for (int i = 0; i < vector.Dim; i++) {
                if (!cond(vector.v[i])) {
                    return false;
                }
            }

            return true;
        }

        public static long Count(ComplexVector vector, Func<Complex, bool> cond) {
            long cnt = 0;

            for (int i = 0; i < vector.Dim; i++) {
                if (cond(vector.v[i])) {
                    cnt++;
                }
            }

            return cnt;
        }
    }
}
