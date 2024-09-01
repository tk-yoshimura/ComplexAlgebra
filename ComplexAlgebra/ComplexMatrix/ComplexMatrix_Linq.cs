using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexMatrix {
        public static bool Any(ComplexMatrix matrix, Func<Complex, bool> cond) {
            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (cond(matrix.e[i, j])) {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool All(ComplexMatrix matrix, Func<Complex, bool> cond) {
            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (!cond(matrix.e[i, j])) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static long Count(ComplexMatrix matrix, Func<Complex, bool> cond) {
            long cnt = 0;

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (cond(matrix.e[i, j])) {
                        cnt++;
                    }
                }
            }

            return cnt;
        }
    }
}
