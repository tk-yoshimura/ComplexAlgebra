using Algebra;
using ComplexAlgebra;

namespace ComplexAlgebraTests {
    public partial class ComplexMatrixTests {
        [TestMethod()]
        public void CholeskyTest() {
            for (int n = 1; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix m = TestCase.RandomMatrix(n, n);
                    ComplexMatrix matrix = m * m.H;

                    Console.WriteLine($"test: {matrix}");

                    ComplexMatrix l = ComplexMatrix.Cholesky(matrix);
                    ComplexMatrix v = l * l.H;

                    Assert.IsTrue((matrix - v).Norm < 1e-25);
                }
            }
        }

        [TestMethod()]
        public void InversePositiveSymmetric() {
            for (int n = 1; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix m = TestCase.RandomMatrix(n, n);
                    ComplexMatrix matrix = m * m.H;
                    Console.WriteLine($"test: {matrix}");

                    ComplexMatrix r = ComplexMatrix.InversePositiveSymmetric(matrix);

                    Assert.IsTrue((matrix * r - ComplexMatrix.Identity(matrix.Size)).Norm < 1e-25);
                }
            }
        }

        [TestMethod()]
        public void SlovePositiveSymmetric() {
            for (int n = 1; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix m = TestCase.RandomMatrix(n, n);
                    ComplexMatrix matrix = m * m.H;

                    Console.WriteLine($"test: {matrix}");

                    ComplexVector v = ComplexVector.Zero(m.Size);
                    for (int j = 0; j < v.Dim; j++) {
                        v[j] = (j + 2, -j + 3);
                    }

                    ComplexMatrix r = ComplexMatrix.InversePositiveSymmetric(matrix);

                    ComplexVector u = ComplexMatrix.SolvePositiveSymmetric(matrix, v);
                    ComplexVector t = r * v;

                    Assert.IsTrue((t - u).Norm < 1e-25);
                }
            }
        }
    }
}