using ComplexAlgebra;
using DoubleDouble;
using DoubleDoubleComplex;

namespace ComplexAlgebraTests {
    public partial class ComplexMatrixTests {
        [TestMethod()]
        public void SVD4x4Test() {
            ComplexMatrix m = new(new Complex[,]
                {{ ( 1, +2), ( 3, -1), ( 5, +0), ( 4, +2) },
                 { ( 4, +0), ( 6, +2), ( 8, -3), ( 3, +1) },
                 { ( 7, -1), ( 9, +1), (11, -2), (12, -3) },
                 { ( 6, -2), ( 4, -1), ( 7,  1), ( 0,  3) } }
            );

            (ComplexMatrix u, ComplexVector s, ComplexMatrix v) = ComplexMatrix.SVD(m);

            ComplexMatrix uu = u * u.H;
            ComplexMatrix vv = v * v.H;

            ComplexMatrix a = u * ComplexMatrix.FromDiagonals(s) * v.H;

            Assert.IsTrue((u.Det.Norm - 1) < 1e-27);
            Assert.IsTrue((uu - ComplexMatrix.Identity(4)).Det.Norm < 1e-27);

            Assert.IsTrue((v.Det.Norm - 1) < 1e-27);
            Assert.IsTrue((vv - ComplexMatrix.Identity(4)).Det.Norm < 1e-27);

            Assert.IsTrue(ddouble.Abs((m - a).Norm) < 1e-27);
        }

        [TestMethod()]
        public void SVD5x4Test() {
            ComplexMatrix m = new(new Complex[,]
                {{ ( 1, +2), ( 3, -1), ( 5, +0), ( 4, +2) },
                 { ( 4, +0), ( 6, +2), ( 8, -3), ( 3, +1) },
                 { ( 7, -1), ( 9, +1), (11, -2), (12, -3) },
                 { (10, +0), (12, -1), (14, +2), ( 7, -4) },
                 { ( 6, -2), ( 4, -1), ( 7,  1), ( 0,  3) } }
            );

            (ComplexMatrix u, ComplexVector s, ComplexMatrix v) = ComplexMatrix.SVD(m);

            ComplexMatrix uu = u * u.H;
            ComplexMatrix vv = v * v.H;

            ComplexMatrix sm = ComplexMatrix.VConcat(ComplexMatrix.FromDiagonals(s), ComplexMatrix.Zero(1, 4));
            ComplexMatrix a = u * sm * v.H;

            Assert.IsTrue((u.Det.Norm - 1) < 1e-27);
            Assert.IsTrue((uu - ComplexMatrix.Identity(5)).Det.Norm < 1e-27);

            Assert.IsTrue((v.Det.Norm - 1) < 1e-27);
            Assert.IsTrue((vv - ComplexMatrix.Identity(4)).Det.Norm < 1e-27);

            Assert.IsTrue(ddouble.Abs((m - a).Norm) < 1e-27);
        }

        [TestMethod()]
        public void SVD4x5Test() {
            ComplexMatrix m = new(new Complex[,]
                {{ ( 1, +2), ( 3, -1), ( 5, +0), ( 4, +2), (6, -2) },
                 { ( 4, +0), ( 6, +2), ( 8, -3), ( 3, +1), (4, -1) },
                 { ( 7, -1), ( 9, +1), (11, -2), (12, -3), (7,  1) },
                 { (10, +0), (12, -1), (14, +2), ( 7, -4), (0,  3) } }
            );

            (ComplexMatrix u, ComplexVector s, ComplexMatrix v) = ComplexMatrix.SVD(m);

            ComplexMatrix uu = u * u.H;
            ComplexMatrix vv = v * v.H;

            ComplexMatrix sm = ComplexMatrix.HConcat(ComplexMatrix.FromDiagonals(s), ComplexMatrix.Zero(4, 1));
            ComplexMatrix a = u * sm * v.H;

            Assert.IsTrue((u.Det.Norm - 1) < 1e-27);
            Assert.IsTrue((uu - ComplexMatrix.Identity(4)).Det.Norm < 1e-27);

            Assert.IsTrue((v.Det.Norm - 1) < 1e-27);
            Assert.IsTrue((vv - ComplexMatrix.Identity(5)).Det.Norm < 1e-27);

            Assert.IsTrue(ddouble.Abs((m - a).Norm) < 1e-27);
        }

        [TestMethod()]
        public void SVDTest() {
            for (int rows = 1; rows <= 8; rows++) {
                for (int cols = 1; cols <= 8; cols++) {
                    if (int.Abs(rows - cols) > 2) {
                        continue;
                    }

                    Console.WriteLine($"{rows}x{cols}");

                    for (int i = 0; i < 4; i++) {
                        ComplexMatrix m = TestCase.RandomMatrix(rows, cols);

                        Console.WriteLine(m);

                        (ComplexMatrix u, ComplexVector s, ComplexMatrix v) = ComplexMatrix.SVD(m);

                        ComplexMatrix uu = u * u.H;
                        ComplexMatrix vv = v * v.H;

                        ComplexMatrix sm;

                        if (rows == cols) {
                            sm = ComplexMatrix.FromDiagonals(s);
                        }
                        else if (rows < cols) {
                            sm = ComplexMatrix.HConcat(ComplexMatrix.FromDiagonals(s), ComplexMatrix.Zero(rows, cols - rows));
                        }
                        else {
                            sm = ComplexMatrix.VConcat(ComplexMatrix.FromDiagonals(s), ComplexMatrix.Zero(rows - cols, cols));
                        }

                        ComplexMatrix a = u * sm * v.H;

                        Console.WriteLine(uu);
                        Console.WriteLine(vv);
                        Console.WriteLine(a);

                        Assert.IsTrue((u.Det.Norm - 1) < 1e-27);
                        Assert.IsTrue((uu - ComplexMatrix.Identity(rows)).Det.Norm < 1e-27);

                        Assert.IsTrue((v.Det.Norm - 1) < 1e-27);
                        Assert.IsTrue((vv - ComplexMatrix.Identity(cols)).Det.Norm < 1e-27);

                        Assert.IsTrue(ddouble.Abs((m - a).Norm) < 1e-27);

                        Console.WriteLine("");
                    }
                }
            }
        }
    }
}