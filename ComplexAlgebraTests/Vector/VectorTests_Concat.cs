using Algebra;
using ComplexAlgebra;
using DoubleDouble;
using DoubleDoubleComplex;

namespace ComplexAlgebraTests {
    public partial class ComplexVectorTests {
        [TestMethod()]
        public void ConcatTest() {
            ComplexVector vector1 = ComplexVector.Fill(1, value: -1);
            ComplexVector vector2 = ComplexVector.Fill(2, value: -2);
            ComplexVector vector4 = ComplexVector.Fill(4, value: -3);

            Assert.AreEqual(new ComplexVector(-1, -2, -2, -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, vector4));
            Assert.AreEqual(new ComplexVector(-2, -2, -3, -3, -3, -3, -1), ComplexVector.Concat(vector2, vector4, vector1));
            Assert.AreEqual(new ComplexVector(-1, -2, -2, 1, -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, 1, vector4));
            Assert.AreEqual(new ComplexVector(-1, -2, -2, 2, -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, 2L, vector4));
            Assert.AreEqual(new ComplexVector(-1, -2, -2, 3, -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, (ddouble)3, vector4));
            Assert.AreEqual(new ComplexVector(-1, -2, -2, Complex.ImaginaryOne, -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, Complex.ImaginaryOne, vector4));
            Assert.AreEqual(new ComplexVector(-1, -2, -2, 4, -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, 4d, vector4));
            Assert.AreEqual(new ComplexVector(-1, -2, -2, 5, -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, 5f, vector4));
            Assert.AreEqual(new ComplexVector(-1, -2, -2, "6.2", -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, "6.2", vector4));

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector.Concat(vector1, vector2, 'b', vector4);
            });
        }

        [TestMethod()]
        public void ConcatRealTest() {
            Vector vector1 = Vector.Fill(1, value: -1);
            Vector vector2 = Vector.Fill(2, value: -2);
            Vector vector4 = Vector.Fill(4, value: -3);

            Assert.AreEqual(new ComplexVector(-1, -2, -2, -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, vector4));
            Assert.AreEqual(new ComplexVector(-2, -2, -3, -3, -3, -3, -1), ComplexVector.Concat(vector2, vector4, vector1));
            Assert.AreEqual(new ComplexVector(-1, -2, -2, 1, -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, 1, vector4));
            Assert.AreEqual(new ComplexVector(-1, -2, -2, 2, -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, 2L, vector4));
            Assert.AreEqual(new ComplexVector(-1, -2, -2, 3, -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, (ddouble)3, vector4));
            Assert.AreEqual(new ComplexVector(-1, -2, -2, Complex.ImaginaryOne, -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, Complex.ImaginaryOne, vector4));
            Assert.AreEqual(new ComplexVector(-1, -2, -2, 4, -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, 4d, vector4));
            Assert.AreEqual(new ComplexVector(-1, -2, -2, 5, -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, 5f, vector4));
            Assert.AreEqual(new ComplexVector(-1, -2, -2, "6.2", -3, -3, -3, -3), ComplexVector.Concat(vector1, vector2, "6.2", vector4));

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector.Concat(vector1, vector2, 'b', vector4);
            });
        }
    }
}