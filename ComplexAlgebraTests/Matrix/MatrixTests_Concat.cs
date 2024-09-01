using Algebra;
using ComplexAlgebra;
using DoubleDouble;
using DoubleDoubleComplex;

namespace ComplexAlgebraTests {
    public partial class ComplexMatrixTests {
        [TestMethod()]
        public void ConcatTest() {
            ComplexMatrix matrix3x2 = ComplexMatrix.Fill(3, 2, value: 2);
            ComplexMatrix matrix3x4 = ComplexMatrix.Fill(3, 4, value: 3);
            ComplexMatrix matrix3x6 = ComplexMatrix.Fill(3, 6, value: 4);
            ComplexMatrix matrix5x2 = ComplexMatrix.Fill(5, 2, value: 5);
            ComplexMatrix matrix5x4 = ComplexMatrix.Fill(5, 4, value: 6);
            ComplexMatrix matrix5x6 = ComplexMatrix.Fill(5, 6, value: 7);
            ComplexMatrix matrix7x2 = ComplexMatrix.Fill(7, 2, value: 8);
            ComplexMatrix matrix7x4 = ComplexMatrix.Fill(7, 4, value: 9);
            ComplexMatrix matrix7x6 = ComplexMatrix.Fill(7, 6, value: 10);

            ComplexVector vector3x1 = ComplexVector.Fill(3, value: -1);
            ComplexVector vector5x1 = ComplexVector.Fill(5, value: -2);
            ComplexVector vector7x1 = ComplexVector.Fill(7, value: -3);

            ComplexMatrix matrix1 = ComplexMatrix.Concat(new object[,] { { matrix3x2 }, { matrix5x2 }, { matrix7x2 } });
            Assert.AreEqual(matrix3x2, matrix1[..3, ..]);
            Assert.AreEqual(matrix5x2, matrix1[3..8, ..]);
            Assert.AreEqual(matrix7x2, matrix1[8.., ..]);

            ComplexMatrix matrix2 = ComplexMatrix.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 } });
            Assert.AreEqual(matrix3x2, matrix2[.., ..2]);
            Assert.AreEqual(matrix3x4, matrix2[.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix2[.., 6..]);

            ComplexMatrix matrix3 = ComplexMatrix.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x6 } });
            Assert.AreEqual(matrix3x2, matrix3[..3, ..2]);
            Assert.AreEqual(matrix5x2, matrix3[3..8, ..2]);
            Assert.AreEqual(matrix7x2, matrix3[8.., ..2]);
            Assert.AreEqual(matrix3x4, matrix3[..3, 2..6]);
            Assert.AreEqual(matrix5x4, matrix3[3..8, 2..6]);
            Assert.AreEqual(matrix7x4, matrix3[8.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix3[..3, 6..]);
            Assert.AreEqual(matrix5x6, matrix3[3..8, 6..]);
            Assert.AreEqual(matrix7x6, matrix3[8.., 6..]);

            ComplexMatrix matrix4 = ComplexMatrix.Concat(new object[,] { { matrix3x2, vector3x1, matrix3x6 } });
            Assert.AreEqual(matrix3x2, matrix4[.., ..2]);
            Assert.AreEqual(vector3x1, matrix4[.., 2]);
            Assert.AreEqual(matrix3x6, matrix4[.., 3..]);

            ComplexMatrix matrix5 = ComplexMatrix.Concat(new object[,] { { vector3x1, matrix3x2, matrix3x6 } });
            Assert.AreEqual(vector3x1, matrix5[.., 0]);
            Assert.AreEqual(matrix3x2, matrix5[.., 1..3]);
            Assert.AreEqual(matrix3x6, matrix5[.., 3..]);

            ComplexMatrix matrix6 = ComplexMatrix.Concat(new object[,] { { matrix3x2, matrix3x6, vector3x1 } });
            Assert.AreEqual(matrix3x2, matrix6[.., 0..2]);
            Assert.AreEqual(matrix3x6, matrix6[.., 2..8]);
            Assert.AreEqual(vector3x1, matrix6[.., 8]);

            ComplexMatrix matrix7 = ComplexMatrix.Concat(new object[,] { { matrix3x2, matrix3x4, vector3x1, matrix3x6 }, { matrix5x2, matrix5x4, vector5x1, matrix5x6 }, { matrix7x2, matrix7x4, vector7x1, matrix7x6 } });
            Assert.AreEqual(matrix3x2, matrix7[..3, ..2]);
            Assert.AreEqual(matrix5x2, matrix7[3..8, ..2]);
            Assert.AreEqual(matrix7x2, matrix7[8.., ..2]);
            Assert.AreEqual(matrix3x4, matrix7[..3, 2..6]);
            Assert.AreEqual(matrix5x4, matrix7[3..8, 2..6]);
            Assert.AreEqual(matrix7x4, matrix7[8.., 2..6]);
            Assert.AreEqual(vector3x1, matrix7[..3, 6]);
            Assert.AreEqual(vector5x1, matrix7[3..8, 6]);
            Assert.AreEqual(vector7x1, matrix7[8.., 6]);
            Assert.AreEqual(matrix3x6, matrix7[..3, 7..]);
            Assert.AreEqual(matrix5x6, matrix7[3..8, 7..]);
            Assert.AreEqual(matrix7x6, matrix7[8.., 7..]);

            ComplexMatrix matrix8 = ComplexMatrix.Concat(new object[,] { { vector3x1 }, { vector5x1 }, { vector7x1 } });
            Assert.AreEqual(vector3x1, matrix8[..3, 0]);
            Assert.AreEqual(vector5x1, matrix8[3..8, 0]);
            Assert.AreEqual(vector7x1, matrix8[8.., 0]);

            ComplexMatrix matrix9 = ComplexMatrix.Concat(
                new object[,] {
                    { vector3x1, vector3x1, vector3x1, vector3x1, vector3x1, vector3x1 },
                    { vector5x1, vector5x1, vector5x1, vector5x1, vector5x1, vector5x1 },
                    { (int)(-10), (long)(-20L), ddouble.E, 4.3d, 4.2f, "4.1" },
                    { vector7x1, vector7x1, vector7x1, vector7x1, vector7x1, vector7x1 } }
            );

            Assert.AreEqual(-10, matrix9[8, 0]);
            Assert.AreEqual(-20, matrix9[8, 1]);
            Assert.AreEqual(ddouble.E, matrix9[8, 2]);
            Assert.AreEqual((ddouble)4.3d, matrix9[8, 3]);
            Assert.AreEqual((ddouble)4.2f, matrix9[8, 4]);
            Assert.AreEqual((ddouble)"4.1", matrix9[8, 5]);

            ComplexMatrix matrix10 = ComplexMatrix.Concat(
                new object[,] {
                    { vector3x1, vector3x1, vector3x1, vector3x1, vector3x1, vector3x1 },
                    { (int)(-10), (long)(-20L), ddouble.E, 4.3d, 4.2f, "4.1" },
                    { (int)(-10), (long)(-20L), Complex.ImaginaryOne, 4.3d, 4.2f, "4.1" },
                    { vector5x1, vector5x1, vector5x1, vector5x1, vector5x1, vector5x1 },
                    { vector7x1, vector7x1, vector7x1, vector7x1, vector7x1, vector7x1 } }
            );

            Assert.AreEqual(-10, matrix10[3, 0]);
            Assert.AreEqual(-20, matrix10[3, 1]);
            Assert.AreEqual(ddouble.E, matrix10[3, 2]);
            Assert.AreEqual((ddouble)4.3d, matrix10[3, 3]);
            Assert.AreEqual((ddouble)4.2f, matrix10[3, 4]);
            Assert.AreEqual((ddouble)"4.1", matrix10[3, 5]);
            Assert.AreEqual(Complex.ImaginaryOne, matrix10[4, 2]);

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix.Concat(new object[,] { { matrix3x2, matrix5x2, matrix7x2 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix.Concat(new object[,] { { matrix3x2 }, { matrix3x4 }, { matrix3x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix.Concat(new object[,] { { matrix3x2, matrix5x2, matrix7x2 }, { matrix3x4, matrix5x4, matrix7x4 }, { matrix3x6, matrix5x6, matrix7x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix.Concat(new object[,] { { matrix3x2 }, { matrix5x4 }, { matrix7x2 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix.Concat(new object[,] { { matrix5x2, matrix3x4, matrix3x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x4 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix.Concat(new object[,] { { matrix3x2, 'b', matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x4 } });
            });
        }

        [TestMethod()]
        public void ConcatMatrixTest() {
            ComplexMatrix matrix3x2 = ComplexMatrix.Fill(3, 2, value: 2);
            ComplexMatrix matrix3x4 = ComplexMatrix.Fill(3, 4, value: 3);
            ComplexMatrix matrix3x6 = ComplexMatrix.Fill(3, 6, value: 4);
            ComplexMatrix matrix5x2 = ComplexMatrix.Fill(5, 2, value: 5);
            ComplexMatrix matrix5x4 = ComplexMatrix.Fill(5, 4, value: 6);
            ComplexMatrix matrix5x6 = ComplexMatrix.Fill(5, 6, value: 7);
            ComplexMatrix matrix7x2 = ComplexMatrix.Fill(7, 2, value: 8);
            ComplexMatrix matrix7x4 = ComplexMatrix.Fill(7, 4, value: 9);
            ComplexMatrix matrix7x6 = ComplexMatrix.Fill(7, 6, value: 10);

            ComplexMatrix matrix1 = ComplexMatrix.Concat(new ComplexMatrix[,] { { matrix3x2 }, { matrix5x2 }, { matrix7x2 } });
            Assert.AreEqual(matrix3x2, matrix1[..3, ..]);
            Assert.AreEqual(matrix5x2, matrix1[3..8, ..]);
            Assert.AreEqual(matrix7x2, matrix1[8.., ..]);

            ComplexMatrix matrix2 = ComplexMatrix.Concat(new ComplexMatrix[,] { { matrix3x2, matrix3x4, matrix3x6 } });
            Assert.AreEqual(matrix3x2, matrix2[.., ..2]);
            Assert.AreEqual(matrix3x4, matrix2[.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix2[.., 6..]);

            ComplexMatrix matrix3 = ComplexMatrix.Concat(new ComplexMatrix[,] { { matrix3x2, matrix3x4, matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x6 } });
            Assert.AreEqual(matrix3x2, matrix3[..3, ..2]);
            Assert.AreEqual(matrix5x2, matrix3[3..8, ..2]);
            Assert.AreEqual(matrix7x2, matrix3[8.., ..2]);
            Assert.AreEqual(matrix3x4, matrix3[..3, 2..6]);
            Assert.AreEqual(matrix5x4, matrix3[3..8, 2..6]);
            Assert.AreEqual(matrix7x4, matrix3[8.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix3[..3, 6..]);
            Assert.AreEqual(matrix5x6, matrix3[3..8, 6..]);
            Assert.AreEqual(matrix7x6, matrix3[8.., 6..]);
        }

        [TestMethod()]
        public void VConcatTest() {
            ComplexMatrix matrix = ComplexMatrix.VConcat(
                ComplexVector.Fill(3, 1),
                ComplexVector.Fill(3, 2),
                ComplexVector.Fill(3, 3),
                ComplexVector.Fill(3, 4),
                ComplexVector.Fill(3, 5)
            );

            ComplexMatrix matrix_expected = new Complex[5, 3] {
                { 1, 1, 1 },
                { 2, 2, 2 },
                { 3, 3, 3 },
                { 4, 4, 4 },
                { 5, 5, 5 },
            };

            Console.WriteLine(matrix);

            Assert.AreEqual(matrix_expected, matrix);
        }

        [TestMethod()]
        public void HConcatTest() {
            ComplexMatrix matrix = ComplexMatrix.HConcat(
                ComplexVector.Fill(3, 1),
                ComplexVector.Fill(3, 2),
                ComplexVector.Fill(3, 3),
                ComplexVector.Fill(3, 4),
                ComplexVector.Fill(3, 5)
            );

            ComplexMatrix matrix_expected = new Complex[3, 5] {
                { 1, 2, 3, 4, 5 },
                { 1, 2, 3, 4, 5 },
                { 1, 2, 3, 4, 5 }
            };

            Console.WriteLine(matrix);

            Assert.AreEqual(matrix_expected, matrix);
        }

        [TestMethod()]
        public void ConcatRealTest() {
            ComplexMatrix matrix3x2 = ComplexMatrix.Fill(3, 2, value: 2);
            ComplexMatrix matrix3x4 = ComplexMatrix.Fill(3, 4, value: 3);
            ComplexMatrix matrix3x6 = ComplexMatrix.Fill(3, 6, value: 4);
            ComplexMatrix matrix5x2 = ComplexMatrix.Fill(5, 2, value: 5);
            ComplexMatrix matrix5x4 = ComplexMatrix.Fill(5, 4, value: 6);
            ComplexMatrix matrix5x6 = ComplexMatrix.Fill(5, 6, value: 7);
            ComplexMatrix matrix7x2 = ComplexMatrix.Fill(7, 2, value: 8);
            ComplexMatrix matrix7x4 = ComplexMatrix.Fill(7, 4, value: 9);
            ComplexMatrix matrix7x6 = ComplexMatrix.Fill(7, 6, value: 10);

            Vector vector3x1 = Vector.Fill(3, value: -1);
            Vector vector5x1 = Vector.Fill(5, value: -2);
            Vector vector7x1 = Vector.Fill(7, value: -3);

            ComplexMatrix matrix1 = ComplexMatrix.Concat(new object[,] { { matrix3x2 }, { matrix5x2 }, { matrix7x2 } });
            Assert.AreEqual(matrix3x2, matrix1[..3, ..]);
            Assert.AreEqual(matrix5x2, matrix1[3..8, ..]);
            Assert.AreEqual(matrix7x2, matrix1[8.., ..]);

            ComplexMatrix matrix2 = ComplexMatrix.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 } });
            Assert.AreEqual(matrix3x2, matrix2[.., ..2]);
            Assert.AreEqual(matrix3x4, matrix2[.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix2[.., 6..]);

            ComplexMatrix matrix3 = ComplexMatrix.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x6 } });
            Assert.AreEqual(matrix3x2, matrix3[..3, ..2]);
            Assert.AreEqual(matrix5x2, matrix3[3..8, ..2]);
            Assert.AreEqual(matrix7x2, matrix3[8.., ..2]);
            Assert.AreEqual(matrix3x4, matrix3[..3, 2..6]);
            Assert.AreEqual(matrix5x4, matrix3[3..8, 2..6]);
            Assert.AreEqual(matrix7x4, matrix3[8.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix3[..3, 6..]);
            Assert.AreEqual(matrix5x6, matrix3[3..8, 6..]);
            Assert.AreEqual(matrix7x6, matrix3[8.., 6..]);

            ComplexMatrix matrix4 = ComplexMatrix.Concat(new object[,] { { matrix3x2, vector3x1, matrix3x6 } });
            Assert.AreEqual(matrix3x2, matrix4[.., ..2]);
            Assert.AreEqual(vector3x1, matrix4[.., 2]);
            Assert.AreEqual(matrix3x6, matrix4[.., 3..]);

            ComplexMatrix matrix5 = ComplexMatrix.Concat(new object[,] { { vector3x1, matrix3x2, matrix3x6 } });
            Assert.AreEqual(vector3x1, matrix5[.., 0]);
            Assert.AreEqual(matrix3x2, matrix5[.., 1..3]);
            Assert.AreEqual(matrix3x6, matrix5[.., 3..]);

            ComplexMatrix matrix6 = ComplexMatrix.Concat(new object[,] { { matrix3x2, matrix3x6, vector3x1 } });
            Assert.AreEqual(matrix3x2, matrix6[.., 0..2]);
            Assert.AreEqual(matrix3x6, matrix6[.., 2..8]);
            Assert.AreEqual(vector3x1, matrix6[.., 8]);

            ComplexMatrix matrix7 = ComplexMatrix.Concat(new object[,] { { matrix3x2, matrix3x4, vector3x1, matrix3x6 }, { matrix5x2, matrix5x4, vector5x1, matrix5x6 }, { matrix7x2, matrix7x4, vector7x1, matrix7x6 } });
            Assert.AreEqual(matrix3x2, matrix7[..3, ..2]);
            Assert.AreEqual(matrix5x2, matrix7[3..8, ..2]);
            Assert.AreEqual(matrix7x2, matrix7[8.., ..2]);
            Assert.AreEqual(matrix3x4, matrix7[..3, 2..6]);
            Assert.AreEqual(matrix5x4, matrix7[3..8, 2..6]);
            Assert.AreEqual(matrix7x4, matrix7[8.., 2..6]);
            Assert.AreEqual(vector3x1, matrix7[..3, 6]);
            Assert.AreEqual(vector5x1, matrix7[3..8, 6]);
            Assert.AreEqual(vector7x1, matrix7[8.., 6]);
            Assert.AreEqual(matrix3x6, matrix7[..3, 7..]);
            Assert.AreEqual(matrix5x6, matrix7[3..8, 7..]);
            Assert.AreEqual(matrix7x6, matrix7[8.., 7..]);

            ComplexMatrix matrix8 = ComplexMatrix.Concat(new object[,] { { vector3x1 }, { vector5x1 }, { vector7x1 } });
            Assert.AreEqual(vector3x1, matrix8[..3, 0]);
            Assert.AreEqual(vector5x1, matrix8[3..8, 0]);
            Assert.AreEqual(vector7x1, matrix8[8.., 0]);

            ComplexMatrix matrix9 = ComplexMatrix.Concat(
                new object[,] {
                    { vector3x1, vector3x1, vector3x1, vector3x1, vector3x1, vector3x1 },
                    { vector5x1, vector5x1, vector5x1, vector5x1, vector5x1, vector5x1 },
                    { (int)(-10), (long)(-20L), ddouble.E, 4.3d, 4.2f, "4.1" },
                    { vector7x1, vector7x1, vector7x1, vector7x1, vector7x1, vector7x1 } }
            );

            Assert.AreEqual(-10, matrix9[8, 0]);
            Assert.AreEqual(-20, matrix9[8, 1]);
            Assert.AreEqual(ddouble.E, matrix9[8, 2]);
            Assert.AreEqual((ddouble)4.3d, matrix9[8, 3]);
            Assert.AreEqual((ddouble)4.2f, matrix9[8, 4]);
            Assert.AreEqual((ddouble)"4.1", matrix9[8, 5]);

            ComplexMatrix matrix10 = ComplexMatrix.Concat(
                new object[,] {
                    { vector3x1, vector3x1, vector3x1, vector3x1, vector3x1, vector3x1 },
                    { (int)(-10), (long)(-20L), ddouble.E, 4.3d, 4.2f, "4.1" },
                    { vector5x1, vector5x1, vector5x1, vector5x1, vector5x1, vector5x1 },
                    { vector7x1, vector7x1, vector7x1, vector7x1, vector7x1, vector7x1 } }
            );

            Assert.AreEqual(-10, matrix10[3, 0]);
            Assert.AreEqual(-20, matrix10[3, 1]);
            Assert.AreEqual(ddouble.E, matrix10[3, 2]);
            Assert.AreEqual((ddouble)4.3d, matrix10[3, 3]);
            Assert.AreEqual((ddouble)4.2f, matrix10[3, 4]);
            Assert.AreEqual((ddouble)"4.1", matrix10[3, 5]);

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix.Concat(new object[,] { { matrix3x2, matrix5x2, matrix7x2 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix.Concat(new object[,] { { matrix3x2 }, { matrix3x4 }, { matrix3x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix.Concat(new object[,] { { matrix3x2, matrix5x2, matrix7x2 }, { matrix3x4, matrix5x4, matrix7x4 }, { matrix3x6, matrix5x6, matrix7x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix.Concat(new object[,] { { matrix3x2 }, { matrix5x4 }, { matrix7x2 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix.Concat(new object[,] { { matrix5x2, matrix3x4, matrix3x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x4 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexMatrix.Concat(new object[,] { { matrix3x2, 'b', matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x4 } });
            });
        }

        [TestMethod()]
        public void ConcatMatrixRealTest() {
            Matrix matrix3x2 = Matrix.Fill(3, 2, value: 2);
            Matrix matrix3x4 = Matrix.Fill(3, 4, value: 3);
            Matrix matrix3x6 = Matrix.Fill(3, 6, value: 4);
            Matrix matrix5x2 = Matrix.Fill(5, 2, value: 5);
            Matrix matrix5x4 = Matrix.Fill(5, 4, value: 6);
            Matrix matrix5x6 = Matrix.Fill(5, 6, value: 7);
            Matrix matrix7x2 = Matrix.Fill(7, 2, value: 8);
            Matrix matrix7x4 = Matrix.Fill(7, 4, value: 9);
            Matrix matrix7x6 = Matrix.Fill(7, 6, value: 10);

            ComplexMatrix matrix1 = ComplexMatrix.Concat(new ComplexMatrix[,] { { matrix3x2 }, { matrix5x2 }, { matrix7x2 } });
            Assert.AreEqual(matrix3x2, matrix1[..3, ..]);
            Assert.AreEqual(matrix5x2, matrix1[3..8, ..]);
            Assert.AreEqual(matrix7x2, matrix1[8.., ..]);

            ComplexMatrix matrix2 = ComplexMatrix.Concat(new ComplexMatrix[,] { { matrix3x2, matrix3x4, matrix3x6 } });
            Assert.AreEqual(matrix3x2, matrix2[.., ..2]);
            Assert.AreEqual(matrix3x4, matrix2[.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix2[.., 6..]);

            ComplexMatrix matrix3 = ComplexMatrix.Concat(new ComplexMatrix[,] { { matrix3x2, matrix3x4, matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x6 } });
            Assert.AreEqual(matrix3x2, matrix3[..3, ..2]);
            Assert.AreEqual(matrix5x2, matrix3[3..8, ..2]);
            Assert.AreEqual(matrix7x2, matrix3[8.., ..2]);
            Assert.AreEqual(matrix3x4, matrix3[..3, 2..6]);
            Assert.AreEqual(matrix5x4, matrix3[3..8, 2..6]);
            Assert.AreEqual(matrix7x4, matrix3[8.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix3[..3, 6..]);
            Assert.AreEqual(matrix5x6, matrix3[3..8, 6..]);
            Assert.AreEqual(matrix7x6, matrix3[8.., 6..]);
        }

        [TestMethod()]
        public void VConcatRealTest() {
            ComplexMatrix matrix = ComplexMatrix.VConcat(
                Vector.Fill(3, 1),
                Vector.Fill(3, 2),
                Vector.Fill(3, 3),
                Vector.Fill(3, 4),
                Vector.Fill(3, 5)
            );

            ComplexMatrix matrix_expected = new Complex[5, 3] {
                { 1, 1, 1 },
                { 2, 2, 2 },
                { 3, 3, 3 },
                { 4, 4, 4 },
                { 5, 5, 5 },
            };

            Console.WriteLine(matrix);

            Assert.AreEqual(matrix_expected, matrix);
        }

        [TestMethod()]
        public void HConcatRealTest() {
            ComplexMatrix matrix = ComplexMatrix.HConcat(
                Vector.Fill(3, 1),
                Vector.Fill(3, 2),
                Vector.Fill(3, 3),
                Vector.Fill(3, 4),
                Vector.Fill(3, 5)
            );

            ComplexMatrix matrix_expected = new Complex[3, 5] {
                { 1, 2, 3, 4, 5 },
                { 1, 2, 3, 4, 5 },
                { 1, 2, 3, 4, 5 }
            };

            Console.WriteLine(matrix);

            Assert.AreEqual(matrix_expected, matrix);
        }
    }
}