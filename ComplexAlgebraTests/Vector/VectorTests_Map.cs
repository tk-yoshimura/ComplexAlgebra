using ComplexAlgebra;

namespace ComplexAlgebraTests {
    public partial class ComplexVectorTests {
        [TestMethod()]
        public void FuncTest() {
            ComplexVector vector1 = new(1, 2, 4, 8);
            ComplexVector vector2 = new(2, 3, 5, 9);
            ComplexVector vector3 = new(3, 4, 6, 10);
            ComplexVector vector4 = new(4, 5, 7, 11);
            ComplexVector vector5 = new(5, 6, 8, 12, 20);

            Assert.AreEqual(new ComplexVector(2, 4, 8, 16), ComplexVector.Func(v => 2 * v, vector1));
            Assert.AreEqual(new ComplexVector(5, 8, 14, 26), ComplexVector.Func((v1, v2) => v1 + 2 * v2, vector1, vector2));
            Assert.AreEqual(new ComplexVector(17, 24, 38, 66), ComplexVector.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, vector1, vector2, vector3));
            Assert.AreEqual(new ComplexVector(49, 64, 94, 154), ComplexVector.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector2, vector3, vector4));
            Assert.AreEqual(new ComplexVector(49, 64, 94, 154), ComplexVector.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector2, vector3, vector4));

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector.Func((v1, v2) => v1 + 2 * v2, vector1, vector5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector.Func((v1, v2) => v1 + 2 * v2, vector5, vector1);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, vector1, vector2, vector5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, vector1, vector5, vector2);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, vector5, vector1, vector2);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector2, vector3, vector5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector2, vector5, vector3);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector5, vector2, vector3);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                ComplexVector.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector5, vector1, vector2, vector3);
            });
        }
    }
}