using ComplexAlgebra;
using DoubleDoubleComplex;

namespace ComplexAlgebraTests {
    public partial class ComplexMatrixTests {
        [TestMethod()]
        public void EigenValuesN2Test() {
            for (int i = 0; i < 64; i++) {
                ComplexMatrix m = TestCase.RandomMatrix(2, 2);

                if (m.Any(v => Complex.IsZero(v.val))) {
                    continue;
                }

                if (i < 4) {
                    m[1, 0] = 0;
                }

                (Complex[] vals, ComplexVector[] vecs) = ComplexMatrix.EigenValueVectors(m);

                Console.WriteLine(m);

                for (int j = 0; j < vals.Length; j++) {
                    Complex val = vals[j];
                    ComplexVector vec = vecs[j];

                    ComplexVector a = m * vec;
                    ComplexVector b = val * vec;

                    Console.WriteLine(vec);
                    Console.WriteLine(val);
                    Console.WriteLine(a);
                    Console.WriteLine(b);
                    Console.WriteLine("");

                    Assert.IsTrue((a - b).Norm < 1e-27);
                }

                Console.WriteLine("");
            }
        }

        [TestMethod()]
        public void EigenValuesN3Test() {
            ComplexMatrix m = new(new Complex[,]
                {{ "-1-4i", "-2+1i", "-6-2i" },
                 { "7+6i", "2+4i", "-6+6i" },
                 { "6-2i", "4-1i", "7+1i" } }
            );

            (Complex[] vals, ComplexVector[] vecs) = ComplexMatrix.EigenValueVectors(m);

            for (int i = 0; i < vals.Length; i++) {
                Complex val = vals[i];
                ComplexVector vec = vecs[i];

                ComplexVector a = m * vec;
                ComplexVector b = val * vec;

                Console.WriteLine(vec);
                Console.WriteLine(val);
                Console.WriteLine(a);
                Console.WriteLine(b);

                Assert.IsTrue((a - b).Norm < 1e-27);
            }
        }

        [TestMethod()]
        public void EigenValuesN4Test() {
            ComplexMatrix m = new(new Complex[,]
                {{ ( 1, + 2), ( 3, - 1), ( 5, + 0), ( 4, + 2) },
                 { ( 4, + 0), ( 6, + 2), ( 8, - 3), ( 3, + 1) },
                 { ( 7, - 1), ( 9, + 1), (11, - 2), (12, - 3) },
                 { (10, + 0), (12, - 1), (14, + 2), ( 7, - 4) } }
            );

            (Complex[] vals, ComplexVector[] vecs) = ComplexMatrix.EigenValueVectors(m);

            for (int i = 0; i < vals.Length; i++) {
                Complex val = vals[i];
                ComplexVector vec = vecs[i];

                ComplexVector a = m * vec;
                ComplexVector b = val * vec;

                Console.WriteLine(vec);
                Console.WriteLine(val);
                Console.WriteLine(a);
                Console.WriteLine(b);

                Assert.IsTrue((a - b).Norm < 1e-27);
            }
        }

        [TestMethod()]
        public void EigenValuesTest() {
            for (int n = 1; n <= 16; n++) {
                for (int i = 0; i < 32; i++) {
                    ComplexMatrix m = TestCase.RandomMatrix(n, n);

                    if (m.Any(v => Complex.IsZero(v.val))) {
                        continue;
                    }

                    Complex[] vals_withoutvec = ComplexMatrix.EigenValues(m);
                    (Complex[] vals, ComplexVector[] vecs) = ComplexMatrix.EigenValueVectors(m);

                    CollectionAssert.AreEqual(vals_withoutvec, vals);

                    Console.WriteLine(m);

                    for (int j = 0; j < vals.Length; j++) {
                        Complex val = vals[j];
                        ComplexVector vec = vecs[j];

                        ComplexVector a = m * vec;
                        ComplexVector b = val * vec;

                        Console.WriteLine(vec);
                        Console.WriteLine(val);
                        Console.WriteLine(a);
                        Console.WriteLine(b);
                        Console.WriteLine("");

                        Assert.IsTrue((a - b).Norm < 1e-27);
                    }

                    Console.WriteLine("");
                }
            }
        }

        [TestMethod()]
        public void EigenVectorTest() {
            foreach (ComplexMatrix matrix in MatrixTestCases.N2Matrixs) {
                Console.WriteLine($"test: {matrix}");

                ComplexMatrix matrix_scaled = ComplexMatrix.ScaleB(matrix, -matrix.MaxExponent);

                (Complex[] eigen_values, ComplexVector[] eigen_vectors) = ComplexMatrix.EigenValueVectors(matrix_scaled);
                ComplexVector eigen_values_expected = ComplexMatrix.EigenValues(matrix_scaled);

                Assert.IsTrue((eigen_values - eigen_values_expected).Norm < 1e-25);

                for (int i = 0; i < matrix_scaled.Size; i++) {
                    Complex eigen_value = eigen_values[i];
                    ComplexVector eigen_vector = eigen_vectors[i];

                    Assert.IsTrue((matrix_scaled * eigen_vector - eigen_value * eigen_vector).Norm < 1e-28);
                }
            }

            foreach (ComplexMatrix matrix in MatrixTestCases.PositiveMatrixs) {
                Console.WriteLine($"test: {matrix}");

                ComplexMatrix matrix_scaled = ComplexMatrix.ScaleB(matrix, -matrix.MaxExponent);

                (Complex[] eigen_values, ComplexVector[] eigen_vectors) = ComplexMatrix.EigenValueVectors(matrix_scaled);
                ComplexVector eigen_values_expected = ComplexMatrix.EigenValues(matrix_scaled);

                Assert.IsTrue((eigen_values - eigen_values_expected).Norm < 1e-25);

                for (int i = 0; i < matrix_scaled.Size; i++) {
                    Complex eigen_value = eigen_values[i];
                    ComplexVector eigen_vector = eigen_vectors[i];

                    Assert.IsTrue((matrix_scaled * eigen_vector - eigen_value * eigen_vector).Norm < 1e-28);
                }
            }

            foreach (ComplexMatrix matrix in MatrixTestCases.SingularMatrixs[..32]) {
                Console.WriteLine($"test: {matrix}");

                ComplexMatrix matrix_scaled = ComplexMatrix.ScaleB(matrix, -matrix.MaxExponent);

                (Complex[] eigen_values, ComplexVector[] eigen_vectors) = ComplexMatrix.EigenValueVectors(matrix_scaled);
                ComplexVector eigen_values_expected = ComplexMatrix.EigenValues(matrix_scaled);

                Assert.IsTrue((eigen_values - eigen_values_expected).Norm < 1e-25);

                for (int i = 0; i < matrix_scaled.Size; i++) {
                    Complex eigen_value = eigen_values[i];
                    ComplexVector eigen_vector = eigen_vectors[i];

                    Assert.IsTrue((matrix_scaled * eigen_vector - eigen_value * eigen_vector).Norm < 1e-10);
                }
            }
        }
    }
}