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
    }
}