using ComplexAlgebra;
using DoubleDoubleComplex;

namespace ComplexAlgebraTests {
    [TestClass]
    public partial class ComplexMatrixTests {
        [TestMethod]
        public void CreateTest() {
            ComplexMatrix m = new(new Complex[,]
                {{ (1, 2), (-1, 3) } ,
                 { (3, -4), (5, -5) } ,
                 { (4, -7), (2, -3) }}
            );

            Assert.AreEqual((1, 2), m[0, 0]);
            Assert.AreEqual((-1, 3), m[0, 1]);
            Assert.AreEqual((3, -4), m[1, 0]);
            Assert.AreEqual((5, -5), m[1, 1]);
            Assert.AreEqual((4, -7), m[2, 0]);
            Assert.AreEqual((2, -3), m[2, 1]);
        }

        [TestMethod]
        public void CopyTest() {
            ComplexMatrix m1 = new(new Complex[,]
                {{ (1, 2), (-1, 3) } ,
                 { (3, -4), (5, -5) } ,
                 { (4, -7), (2, -3) }}
            );

            ComplexMatrix m2 = m1.Copy();

            Assert.AreEqual(m1, m1);
#pragma warning disable CS1718
            Assert.IsTrue(m1 == m1);
#pragma warning restore CS1718
            Assert.AreEqual(m1, m2);
            Assert.IsTrue(m1 == m2);

            m2[0, 0] = (1, 3);

            Assert.AreNotEqual(m1, m2);
            Assert.IsTrue(m1 != m2);
        }

        [TestMethod()]
        public void SampleTest() {
            // solve for v: Av=x
            ComplexMatrix a = new Complex[,] { { (1, 1), (1, 2) }, { (1, 3), (4, -1) } };
            ComplexVector x = ((4, 2), (-1, 3));

            ComplexVector v = ComplexMatrix.Solve(a, x);

            Console.WriteLine(v);
        }

        [TestMethod]
        public void ConjugateTest() {
            ComplexMatrix m = new(new Complex[,]
                {{ (1, 2), (-1, 3) } ,
                 { (3, -4), (5, -5) } ,
                 { (4, -7), (2, -3) }}
            );

            ComplexMatrix m_conj = m.Conj;

            Assert.AreNotEqual(m, m_conj);

            Assert.AreEqual((1, -2), m_conj[0, 0]);
            Assert.AreEqual((-1, -3), m_conj[0, 1]);
            Assert.AreEqual((3, 4), m_conj[1, 0]);
            Assert.AreEqual((5, 5), m_conj[1, 1]);
            Assert.AreEqual((4, 7), m_conj[2, 0]);
            Assert.AreEqual((2, 3), m_conj[2, 1]);
        }

        [TestMethod]
        public void HermitianTest() {
            ComplexMatrix m = new(new Complex[,]
                {{ (1, 2), (-1, 3) } ,
                 { (3, -4), (5, -5) } ,
                 { (4, -7), (2, -3) }}
            );

            ComplexMatrix m_h = m.H;

            Assert.AreNotEqual(m, m_h);

            Assert.AreEqual((1, -2), m_h[0, 0]);
            Assert.AreEqual((3, 4), m_h[0, 1]);
            Assert.AreEqual((4, 7), m_h[0, 2]);
            Assert.AreEqual((-1, -3), m_h[1, 0]);
            Assert.AreEqual((5, 5), m_h[1, 1]);
            Assert.AreEqual((2, 3), m_h[1, 2]);
        }

        [TestMethod]
        public void IsHermitianTest() {
            ComplexMatrix m = new(new Complex[,]
                {{ 3, (2, 3), (0, -4) } ,
                 { (2, -3), -3, (7, 1) } ,
                 { (0, 4), (7, -1), 4 }}
            );

            Assert.IsTrue(ComplexMatrix.IsHermitian(m));

            m[2, 2] = (4, 1);

            ComplexMatrix mh = m.H;

            Assert.IsFalse(ComplexMatrix.IsHermitian(m));
        }

        [TestMethod()]
        public void FlattenTest() {
            ComplexMatrix matrix = new Complex[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            ComplexVector vector = ComplexMatrix.Flatten(matrix);

            Assert.AreEqual(new ComplexVector(1, 2, 3, 4, 5, 6), vector);
        }

        [TestMethod()]
        public void GridTest() {
            ComplexMatrix matrix = ComplexMatrix.Grid((-5, 4), (-3, 5));

            Assert.AreEqual((10, 9), matrix.Shape);

            Assert.AreEqual((-5, -3), matrix[0, 0]);
            Assert.AreEqual((-5, -2), matrix[0, 1]);
            Assert.AreEqual((-4, -3), matrix[1, 0]);

            Assert.AreEqual((-5, 5), matrix[0, ^1]);
            Assert.AreEqual((-5, 4), matrix[0, ^2]);
            Assert.AreEqual((-4, 5), matrix[1, ^1]);

            Assert.AreEqual((4, -3), matrix[^1, 0]);
            Assert.AreEqual((4, -2), matrix[^1, 1]);
            Assert.AreEqual((3, -3), matrix[^2, 0]);

            Assert.AreEqual((4, 5), matrix[^1, ^1]);
            Assert.AreEqual((4, 4), matrix[^1, ^2]);
            Assert.AreEqual((3, 5), matrix[^2, ^1]);
        }
    }
}