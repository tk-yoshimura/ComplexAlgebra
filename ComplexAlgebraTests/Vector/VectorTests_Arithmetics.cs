using ComplexAlgebra;
using DoubleDouble;
using DoubleDoubleComplex;

namespace ComplexAlgebraTests {
    public partial class ComplexVectorTests {
        [TestMethod]
        public void AddTest() {
            ComplexVector v1 = TestCase.RandomVector(4);
            ComplexVector v2 = TestCase.RandomVector(4);

            ComplexVector v = v1 + v2;

            Assert.AreEqual(v1[0] + v2[0], v[0]);
            Assert.AreEqual(v1[1] + v2[1], v[1]);
            Assert.AreEqual(v1[2] + v2[2], v[2]);
            Assert.AreEqual(v1[3] + v2[3], v[3]);
        }

        [TestMethod]
        public void AddScalarTest() {
            for (int i = 0; i < 4; i++) {
                ComplexVector v = TestCase.RandomVector(2);
                Complex c = TestCase.RandomComplex;
                ddouble r = TestCase.RandomScalar;

                Assert.AreEqual(v[0] + c, (v + c)[0]);
                Assert.AreEqual(v[0] + r, (v + r)[0]);
                Assert.AreEqual(c + v[0], (c + v)[0]);
                Assert.AreEqual(r + v[0], (r + v)[0]);
            }
        }

        [TestMethod]
        public void SubTest() {
            ComplexVector v1 = TestCase.RandomVector(4);
            ComplexVector v2 = TestCase.RandomVector(4);

            ComplexVector v = v1 - v2;

            Assert.AreEqual(v1[0] - v2[0], v[0]);
            Assert.AreEqual(v1[1] - v2[1], v[1]);
            Assert.AreEqual(v1[2] - v2[2], v[2]);
            Assert.AreEqual(v1[3] - v2[3], v[3]);
        }

        [TestMethod]
        public void SubScalarTest() {
            for (int i = 0; i < 4; i++) {
                ComplexVector v = TestCase.RandomVector(2);
                Complex c = TestCase.RandomComplex;
                ddouble r = TestCase.RandomScalar;

                Assert.AreEqual(v[0] - c, (v - c)[0]);
                Assert.AreEqual(v[0] - r, (v - r)[0]);
                Assert.AreEqual(c - v[0], (c - v)[0]);
                Assert.AreEqual(r - v[0], (r - v)[0]);
            }
        }

        [TestMethod]
        public void MulTest() {
            ComplexVector v1 = TestCase.RandomVector(4);
            ComplexVector v2 = TestCase.RandomVector(4);

            ComplexVector v = v1 * v2;

            Assert.AreEqual(v1[0] * v2[0], v[0]);
            Assert.AreEqual(v1[1] * v2[1], v[1]);
            Assert.AreEqual(v1[2] * v2[2], v[2]);
            Assert.AreEqual(v1[3] * v2[3], v[3]);
        }

        [TestMethod]
        public void MulScalarTest() {
            for (int i = 0; i < 4; i++) {
                ComplexVector v = TestCase.RandomVector(2);
                Complex c = TestCase.RandomComplex;
                ddouble r = TestCase.RandomScalar;

                Assert.AreEqual(v[0] * c, (v * c)[0]);
                Assert.AreEqual(v[0] * r, (v * r)[0]);
                Assert.AreEqual(c * v[0], (c * v)[0]);
                Assert.AreEqual(r * v[0], (r * v)[0]);
            }
        }

        [TestMethod]
        public void DivTest() {
            ComplexVector v1 = TestCase.RandomVector(4);
            ComplexVector v2 = TestCase.RandomVector(4);

            ComplexVector v = v1 / v2;

            Assert.AreEqual(v1[0] / v2[0], v[0]);
            Assert.AreEqual(v1[1] / v2[1], v[1]);
            Assert.AreEqual(v1[2] / v2[2], v[2]);
            Assert.AreEqual(v1[3] / v2[3], v[3]);
        }

        [TestMethod]
        public void DivScalarTest() {
            for (int i = 0; i < 16; i++) {
                ComplexVector v = TestCase.RandomVector(2);
                Complex c = TestCase.RandomComplex;
                ddouble r = TestCase.RandomScalar;

                if (r == 0 || c == 0) {
                    continue;
                }

                Assert.IsTrue(((v[0] / c) - ((v / c)[0])).Norm < 1e-30);
                Assert.IsTrue(((v[0] / r) - ((v / r)[0])).Norm < 1e-30);
                Assert.IsTrue(((c / v[0]) - ((c / v)[0])).Norm < 1e-30);
                Assert.IsTrue(((r / v[0]) - ((r / v)[0])).Norm < 1e-30);
            }
        }

        [TestMethod]
        public void UnaryPlusTest() {
            ComplexVector v1 = TestCase.RandomVector(2);
            ComplexVector v2 = +v1;

            Assert.AreEqual(v1[0], v2[0]);
            Assert.AreEqual(v1[1], v2[1]);
        }

        [TestMethod]
        public void UnaryMinusTest() {
            ComplexVector v1 = TestCase.RandomVector(2);
            ComplexVector v2 = -v1;

            Assert.AreEqual(-v1[0], v2[0]);
            Assert.AreEqual(-v1[1], v2[1]);
        }

        [TestMethod]
        public void SumTest() {
            ComplexVector m = TestCase.RandomVector(4);

            Complex v = m.Sum, u = m.Mean;

            Assert.AreEqual(m[0] + m[1] + m[2] + m[3], v);
            Assert.AreEqual((m[0] + m[1] + m[2] + m[3]) / 4, u);
        }

        [TestMethod]
        public void DotTest() {
            ComplexVector a = new Complex[] { (2, 4), (-4, 2), 0 };
            ComplexVector b = new Complex[] { (9, 3), (6, 6), 9 };

            Complex v1 = ComplexVector.Dot(a, b);
            Complex v2 = ComplexVector.Dot(b, a);

            Assert.AreEqual((18, 66), v1);
            Assert.AreEqual((18, -66), v2);
        }
    }
}