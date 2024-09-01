using Algebra;
using ComplexAlgebra;
using DoubleDouble;
using DoubleDoubleComplex;

namespace ComplexAlgebraTests {
    public partial class ComplexMatrixTests {
        [TestMethod]
        public void AddTest() {
            ComplexMatrix m1 = TestCase.RandomMatrix(2, 3);
            ComplexMatrix m2 = TestCase.RandomMatrix(2, 3);

            ComplexMatrix m = m1 + m2;

            Assert.AreEqual(m1[0, 0] + m2[0, 0], m[0, 0]);
            Assert.AreEqual(m1[0, 1] + m2[0, 1], m[0, 1]);
            Assert.AreEqual(m1[0, 2] + m2[0, 2], m[0, 2]);
            Assert.AreEqual(m1[1, 0] + m2[1, 0], m[1, 0]);
            Assert.AreEqual(m1[1, 1] + m2[1, 1], m[1, 1]);
            Assert.AreEqual(m1[1, 2] + m2[1, 2], m[1, 2]);
        }

        [TestMethod]
        public void AddScalarTest() {
            for (int i = 0; i < 4; i++) {
                ComplexMatrix m = TestCase.RandomMatrix(2, 3);
                Complex c = TestCase.RandomComplex;
                ddouble r = TestCase.RandomScalar;

                Assert.AreEqual(m[0, 0] + c, (m + c)[0, 0]);
                Assert.AreEqual(m[0, 0] + r, (m + r)[0, 0]);
                Assert.AreEqual(c + m[0, 0], (c + m)[0, 0]);
                Assert.AreEqual(r + m[0, 0], (r + m)[0, 0]);
            }
        }

        [TestMethod]
        public void SubTest() {
            ComplexMatrix m1 = TestCase.RandomMatrix(2, 3);
            ComplexMatrix m2 = TestCase.RandomMatrix(2, 3);

            ComplexMatrix m = m1 - m2;

            Assert.AreEqual(m1[0, 0] - m2[0, 0], m[0, 0]);
            Assert.AreEqual(m1[0, 1] - m2[0, 1], m[0, 1]);
            Assert.AreEqual(m1[0, 2] - m2[0, 2], m[0, 2]);
            Assert.AreEqual(m1[1, 0] - m2[1, 0], m[1, 0]);
            Assert.AreEqual(m1[1, 1] - m2[1, 1], m[1, 1]);
            Assert.AreEqual(m1[1, 2] - m2[1, 2], m[1, 2]);
        }

        [TestMethod]
        public void SubScalarTest() {
            for (int i = 0; i < 4; i++) {
                ComplexMatrix m = TestCase.RandomMatrix(2, 3);
                Complex c = TestCase.RandomComplex;
                ddouble r = TestCase.RandomScalar;

                Assert.AreEqual(m[0, 0] - c, (m - c)[0, 0]);
                Assert.AreEqual(m[0, 0] - r, (m - r)[0, 0]);
                Assert.AreEqual(c - m[0, 0], (c - m)[0, 0]);
                Assert.AreEqual(r - m[0, 0], (r - m)[0, 0]);
            }
        }

        [TestMethod]
        public void ElementwiseMulTest() {
            ComplexMatrix m1 = TestCase.RandomMatrix(2, 3);
            ComplexMatrix m2 = TestCase.RandomMatrix(2, 3);

            ComplexMatrix m = ComplexMatrix.ElementwiseMul(m1, m2);

            Assert.AreEqual(m1[0, 0] * m2[0, 0], m[0, 0]);
            Assert.AreEqual(m1[0, 1] * m2[0, 1], m[0, 1]);
            Assert.AreEqual(m1[0, 2] * m2[0, 2], m[0, 2]);
            Assert.AreEqual(m1[1, 0] * m2[1, 0], m[1, 0]);
            Assert.AreEqual(m1[1, 1] * m2[1, 1], m[1, 1]);
            Assert.AreEqual(m1[1, 2] * m2[1, 2], m[1, 2]);
        }

        [TestMethod]
        public void MulScalarTest() {
            for (int i = 0; i < 4; i++) {
                ComplexMatrix m = TestCase.RandomMatrix(2, 3);
                Complex c = TestCase.RandomComplex;
                ddouble r = TestCase.RandomScalar;

                Assert.AreEqual(m[0, 0] * c, (m * c)[0, 0]);
                Assert.AreEqual(m[0, 0] * r, (m * r)[0, 0]);
                Assert.AreEqual(c * m[0, 0], (c * m)[0, 0]);
                Assert.AreEqual(r * m[0, 0], (r * m)[0, 0]);
            }
        }

        [TestMethod]
        public void ElementwiseDivTest() {
            ComplexMatrix m1 = TestCase.RandomMatrix(2, 3);
            ComplexMatrix m2 = TestCase.RandomMatrix(2, 3);

            ComplexMatrix m = ComplexMatrix.ElementwiseDiv(m1, m2);

            Assert.AreEqual(m1[0, 0] / m2[0, 0], m[0, 0]);
            Assert.AreEqual(m1[0, 1] / m2[0, 1], m[0, 1]);
            Assert.AreEqual(m1[0, 2] / m2[0, 2], m[0, 2]);
            Assert.AreEqual(m1[1, 0] / m2[1, 0], m[1, 0]);
            Assert.AreEqual(m1[1, 1] / m2[1, 1], m[1, 1]);
            Assert.AreEqual(m1[1, 2] / m2[1, 2], m[1, 2]);
        }

        [TestMethod]
        public void DivScalarTest() {
            for (int i = 0; i < 16; i++) {
                ComplexMatrix m = TestCase.RandomMatrix(2, 3);
                Complex c = TestCase.RandomComplex;
                ddouble r = TestCase.RandomScalar;

                if (r == 0 || c == 0) {
                    continue;
                }

                Assert.IsTrue(((m[0, 0] / c) - ((m / c)[0, 0])).Norm < 1e-30);
                Assert.IsTrue(((m[0, 0] / r) - ((m / r)[0, 0])).Norm < 1e-30);
                Assert.IsTrue(((c / m[0, 0]) - ((c / m)[0, 0])).Norm < 1e-30);
                Assert.IsTrue(((r / m[0, 0]) - ((r / m)[0, 0])).Norm < 1e-30);
            }
        }

        [TestMethod]
        public void UnaryPlusTest() {
            ComplexMatrix m1 = TestCase.RandomMatrix(2, 3);
            ComplexMatrix m2 = +m1;

            Assert.AreEqual(m1[0, 0], m2[0, 0]);
            Assert.AreEqual(m1[0, 1], m2[0, 1]);
            Assert.AreEqual(m1[0, 2], m2[0, 2]);
            Assert.AreEqual(m1[1, 0], m2[1, 0]);
            Assert.AreEqual(m1[1, 1], m2[1, 1]);
            Assert.AreEqual(m1[1, 2], m2[1, 2]);
        }

        [TestMethod]
        public void UnaryMinusTest() {
            ComplexMatrix m1 = TestCase.RandomMatrix(2, 3);
            ComplexMatrix m2 = -m1;

            Assert.AreEqual(-m1[0, 0], m2[0, 0]);
            Assert.AreEqual(-m1[0, 1], m2[0, 1]);
            Assert.AreEqual(-m1[0, 2], m2[0, 2]);
            Assert.AreEqual(-m1[1, 0], m2[1, 0]);
            Assert.AreEqual(-m1[1, 1], m2[1, 1]);
            Assert.AreEqual(-m1[1, 2], m2[1, 2]);
        }

        [TestMethod]
        public void MatMulTest() {
            ComplexMatrix m1 = TestCase.RandomMatrix(2, 3);
            ComplexMatrix m2 = TestCase.RandomMatrix(3, 4);

            ComplexMatrix m = m1 * m2;

            Assert.AreEqual(m1[0, 0] * m2[0, 0] + m1[0, 1] * m2[1, 0] + m1[0, 2] * m2[2, 0], m[0, 0]);
            Assert.AreEqual(m1[1, 0] * m2[0, 0] + m1[1, 1] * m2[1, 0] + m1[1, 2] * m2[2, 0], m[1, 0]);

            Assert.AreEqual(m1[0, 0] * m2[0, 1] + m1[0, 1] * m2[1, 1] + m1[0, 2] * m2[2, 1], m[0, 1]);
            Assert.AreEqual(m1[1, 0] * m2[0, 1] + m1[1, 1] * m2[1, 1] + m1[1, 2] * m2[2, 1], m[1, 1]);

            Assert.AreEqual(m1[0, 0] * m2[0, 2] + m1[0, 1] * m2[1, 2] + m1[0, 2] * m2[2, 2], m[0, 2]);
            Assert.AreEqual(m1[1, 0] * m2[0, 2] + m1[1, 1] * m2[1, 2] + m1[1, 2] * m2[2, 2], m[1, 2]);

            Assert.AreEqual(m1[0, 0] * m2[0, 3] + m1[0, 1] * m2[1, 3] + m1[0, 2] * m2[2, 3], m[0, 3]);
            Assert.AreEqual(m1[1, 0] * m2[0, 3] + m1[1, 1] * m2[1, 3] + m1[1, 2] * m2[2, 3], m[1, 3]);
        }

        [TestMethod]
        public void VectorMulTest() {
            ComplexVector v = TestCase.RandomVector(2);
            ComplexMatrix m = TestCase.RandomMatrix(2, 3);

            ComplexVector x = v * m;

            Assert.AreEqual(v[0] * m[0, 0] + v[1] * m[1, 0], x[0]);
            Assert.AreEqual(v[0] * m[0, 1] + v[1] * m[1, 1], x[1]);
            Assert.AreEqual(v[0] * m[0, 2] + v[1] * m[1, 2], x[2]);
        }

        [TestMethod]
        public void TraceTest() {
            ComplexMatrix m = TestCase.RandomMatrix(4, 4);

            Assert.AreEqual(m[0, 0] + m[1, 1] + m[2, 2] + m[3, 3], m.Trace);
        }

        [TestMethod]
        public void InvertTest() {
            for (int n = 1; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix m = TestCase.RandomMatrix(n, n);

                    if (m.Det.Norm == 0d) {
                        continue;
                    }

                    ComplexMatrix r = m.Inverse;

                    ComplexMatrix k = m * r;

                    Console.WriteLine(k);

                    Assert.IsTrue((k.Det.Norm - 1) < 1e-30);
                    Assert.IsTrue((k - ComplexMatrix.Identity(n)).Det.Norm < 1e-27);
                }
            }
        }

        [TestMethod]
        public void PseudoInvertTest() {
            for (int n = 2; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix m = TestCase.RandomMatrix(n, n - 1);
                    ComplexMatrix r = m.Inverse;

                    ComplexMatrix k = m * r;

                    Console.WriteLine(k);

                    Assert.IsTrue((k.Det.Norm - 1) < 1e-30);
                    Assert.IsTrue((k - ComplexMatrix.Identity(n)).Det.Norm < 1e-27);
                }
            }

            for (int n = 2; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix m = TestCase.RandomMatrix(n - 1, n);
                    ComplexMatrix r = m.Inverse;

                    ComplexMatrix k = r * m;

                    Console.WriteLine(k);

                    Assert.IsTrue((k.Det.Norm - 1) < 1e-30);
                    Assert.IsTrue((k - ComplexMatrix.Identity(n)).Det.Norm < 1e-27);
                }
            }
        }

        [TestMethod]
        public void LUDecompTest() {
            for (int n = 2; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix m = TestCase.RandomMatrix(n, n);

                    (Matrix p, ComplexMatrix l, ComplexMatrix u) =
                        ComplexMatrix.LU(m);

                    ComplexMatrix lu = l * u;

                    Console.WriteLine(p);
                    Console.WriteLine(m);
                    Console.WriteLine(l);
                    Console.WriteLine(u);

                    Console.WriteLine(lu);

                    Console.WriteLine((p * m - lu).Norm);

                    Assert.IsTrue(ddouble.Abs((p * m - lu).Norm) < 1e-27);
                }
            }
        }

        [TestMethod]
        public void QRDecompTest() {
            for (int n = 2; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix m = TestCase.RandomMatrix(n, n);

                    (ComplexMatrix q, ComplexMatrix r) =
                        ComplexMatrix.QR(m);

                    ComplexMatrix qr = q * r;
                    ComplexMatrix u = q * q.H;

                    Console.WriteLine(m);

                    Console.WriteLine(qr);

                    Console.WriteLine((m - qr).Norm);

                    for (int row = 0; row < n; row++) {
                        for (int col = 0; col < row; col++) {
                            Assert.IsTrue(Complex.IsZero(r[row, col]));
                        }
                    }

                    Assert.IsTrue(ddouble.Abs((m - qr).Norm) < 1e-27);
                    Assert.IsTrue((u.Det.Norm - 1) < 1e-27);
                    Assert.IsTrue((u - ComplexMatrix.Identity(n)).Det.Norm < 1e-27);
                }
            }
        }

        [TestMethod]
        public void QRDecompNasZeroTest() {
            ComplexMatrix m = new Complex[,]{
                 { "0",     "10+2i", "2-3i",  "3+3i",  "-5+4i"  },
                 { "6-1i",  "9+6i",  "-3-7i", "-1",    "-9+10i" },
                 { "9+7i",  "9-6i",  "-8+1i", "-8-3i", "10+7i"  },
                 { "-2-4i", "10-3i", "9-7i",  "-4-1i", "3-4i"   },
                 { "-4-7i", "-7i",   "2+6i",  "-6-7i", "-5+2i"  }
            };

            (ComplexMatrix q, ComplexMatrix r) =
                ComplexMatrix.QR(m);

            ComplexMatrix qr = q * r;
            ComplexMatrix u = q * q.H;

            Console.WriteLine(m);

            Console.WriteLine(qr);

            Console.WriteLine((m - qr).Norm);

            Assert.IsTrue(ddouble.Abs((m - qr).Norm) < 1e-27);
            Assert.IsTrue((u.Det.Norm - 1) < 1e-27);
            Assert.IsTrue((u - ComplexMatrix.Identity(5)).Det.Norm < 1e-27);
        }

        [TestMethod]
        public void SolveTest() {
            for (int n = 1; n <= 16; n++) {
                for (int i = 0; i < 16; i++) {
                    ComplexMatrix a = TestCase.RandomMatrix(n, n);
                    ComplexVector v = TestCase.RandomVector(n);
                    ComplexVector r = ComplexMatrix.Solve(a, v);

                    ComplexVector x = a.Inverse * v;

                    if (a.Det.Norm == 0d) {
                        continue;
                    }

                    Console.WriteLine(x);
                    Console.WriteLine(r);

                    Assert.IsTrue(ddouble.Abs((x - r).Norm) < 1e-27);
                }
            }
        }

        [TestMethod]
        public void SumTest() {
            ComplexMatrix m = TestCase.RandomMatrix(2, 3);

            Complex v = m.Sum, u = m.Mean;

            Assert.AreEqual(m[0, 0] + m[0, 1] + m[0, 2] + m[1, 0] + m[1, 1] + m[1, 2], v);
            Assert.AreEqual((m[0, 0] + m[0, 1] + m[0, 2] + m[1, 0] + m[1, 1] + m[1, 2]) / 6, u);
        }
    }
}