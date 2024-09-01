using ComplexAlgebra;
using DoubleDouble;
using DoubleDoubleComplex;

namespace ComplexAlgebraTests {
    public static class TestCase {
        static readonly Random random = new(1234);

        public static ddouble RandomScalar {
            get {
                return random.Next(-10, 11);
            }
        }

        public static Complex RandomComplex {
            get {
                return (random.Next(-10, 11), random.Next(-10, 11));
            }
        }

        public static ComplexVector RandomVector(int size) {
            return new ComplexVector((new Complex[size]).Select(_ => RandomComplex));
        }

        public static ComplexMatrix RandomMatrix(int rows, int columns) {
            Complex[,] m = new Complex[rows, columns];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    m[i, j] = RandomComplex;
                }
            }

            return new ComplexMatrix(m);
        }
    }
}
