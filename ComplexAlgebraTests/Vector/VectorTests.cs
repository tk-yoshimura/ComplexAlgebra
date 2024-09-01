using ComplexAlgebra;
using DoubleDoubleComplex;

namespace ComplexAlgebraTests {
    [TestClass]
    public partial class ComplexVectorTests {
        [TestMethod]
        public void CreateTest() {
            ComplexVector v = new(
                (1, 2),
                (-1, 3),
                (3, -4)
            );

            Assert.AreEqual((1, 2), v[0]);
            Assert.AreEqual((-1, 3), v[1]);
            Assert.AreEqual((3, -4), v[2]);
        }

        [TestMethod]
        public void CopyTest() {
            ComplexVector v1 = new(
                (1, 2),
                (-1, 3),
                (3, -4)
            );

            ComplexVector v2 = v1.Copy();

            Assert.AreEqual(v1, v1);
#pragma warning disable CS1718
            Assert.IsTrue(v1 == v1);
#pragma warning restore CS1718
            Assert.AreEqual(v1, v2);
            Assert.IsTrue(v1 == v2);

            v2[0] = (1, 3);

            Assert.AreNotEqual(v1, v2);
            Assert.IsTrue(v1 != v2);
        }

        [TestMethod]
        public void ConjugateTest() {
            ComplexVector v = new(
                (1, 2),
                (-1, 3),
                (3, -4)
            );

            ComplexVector v_conj = v.Conj;

            Assert.AreNotEqual(v, v_conj);

            Assert.AreEqual((1, -2), v_conj[0]);
            Assert.AreEqual((-1, -3), v_conj[1]);
            Assert.AreEqual((3, 4), v_conj[2]);
        }

        [TestMethod()]
        public void TupleTest() {
            Complex x, y, z, w, e0, e1, e2, e3, e4, e5, e6, e7;

            ComplexVector vector2 = (2, 4);
            (x, y) = vector2;
            Assert.AreEqual((2, 4), (x, y));

            ComplexVector vector3 = (2, 4, 6);
            (x, y, z) = vector3;
            Assert.AreEqual((2, 4, 6), (x, y, z));

            ComplexVector vector4 = (2, 4, 6, 8);
            (x, y, z, w) = vector4;
            Assert.AreEqual((2, 4, 6, 8), (x, y, z, w));

            ComplexVector vector5 = (2, 4, 6, 8, 1);
            (e0, e1, e2, e3, e4) = vector5;
            Assert.AreEqual((2, 4, 6, 8, 1), (e0, e1, e2, e3, e4));

            ComplexVector vector6 = (2, 4, 6, 8, 1, 3);
            (e0, e1, e2, e3, e4, e5) = vector6;
            Assert.AreEqual((2, 4, 6, 8, 1, 3), (e0, e1, e2, e3, e4, e5));

            ComplexVector vector7 = (2, 4, 6, 8, 1, 3, 5);
            (e0, e1, e2, e3, e4, e5, e6) = vector7;
            Assert.AreEqual((2, 4, 6, 8, 1, 3, 5), (e0, e1, e2, e3, e4, e5, e6));

            ComplexVector vector8 = (2, 4, 6, 8, 1, 3, 5, 7);
            (e0, e1, e2, e3, e4, e5, e6, e7) = vector8;
            Assert.AreEqual((2, 4, 6, 8, 1, 3, 5, 7), (e0, e1, e2, e3, e4, e5, e6, e7));

            Assert.ThrowsException<InvalidOperationException>(() => {
                (x, y, z) = vector2;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (x, y, z, w) = vector3;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4) = vector4;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4, e5) = vector5;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4, e5, e6) = vector6;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4, e5, e6, e7) = vector7;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4, e5, e6) = vector8;
            });
        }
    }
}