﻿using ComplexAlgebra;

namespace ComplexAlgebraTests {
    public partial class ComplexVectorTests {
        [TestMethod()]
        public void RangeIndexerGetterTest() {
            ComplexVector vector = new(1, 2, 3, 4, 5);

            Assert.AreEqual(new ComplexVector(1, 2, 3, 4, 5), vector[..]);

            Assert.AreEqual(new ComplexVector(2, 3, 4, 5), vector[1..]);
            Assert.AreEqual(new ComplexVector(3, 4, 5), vector[2..]);
            Assert.AreEqual(new ComplexVector(1, 2, 3, 4), vector[..^1]);
            Assert.AreEqual(new ComplexVector(1, 2, 3, 4), vector[..4]);
            Assert.AreEqual(new ComplexVector(1, 2, 3), vector[..^2]);
            Assert.AreEqual(new ComplexVector(1, 2, 3), vector[..3]);

            Assert.AreEqual(new ComplexVector(2, 3, 4), vector[1..4]);
            Assert.AreEqual(new ComplexVector(2, 3, 4), vector[1..^1]);
        }

        [TestMethod()]
        public void RangeIndexerSetterTest() {
            ComplexVector vector_src = new(1, 2, 3, 4, 5);
            ComplexVector vector_dst;

            vector_dst = ComplexVector.Zero(vector_src.Dim);
            vector_dst[..] = vector_src;
            Assert.AreEqual(new ComplexVector(1, 2, 3, 4, 5), vector_dst);

            vector_dst = ComplexVector.Zero(vector_src.Dim);
            vector_dst[1..] = vector_src[1..];
            Assert.AreEqual(new ComplexVector(0, 2, 3, 4, 5), vector_dst);

            vector_dst = ComplexVector.Zero(vector_src.Dim);
            vector_dst[2..] = vector_src[2..];
            Assert.AreEqual(new ComplexVector(0, 0, 3, 4, 5), vector_dst);

            vector_dst = ComplexVector.Zero(vector_src.Dim);
            vector_dst[..^1] = vector_src[..^1];
            Assert.AreEqual(new ComplexVector(1, 2, 3, 4, 0), vector_dst);

            vector_dst = ComplexVector.Zero(vector_src.Dim);
            vector_dst[..4] = vector_src[..4];
            Assert.AreEqual(new ComplexVector(1, 2, 3, 4, 0), vector_dst);

            vector_dst = ComplexVector.Zero(vector_src.Dim);
            vector_dst[..^2] = vector_src[..^2];
            Assert.AreEqual(new ComplexVector(1, 2, 3, 0, 0), vector_dst);

            vector_dst = ComplexVector.Zero(vector_src.Dim);
            vector_dst[..3] = vector_src[..3];
            Assert.AreEqual(new ComplexVector(1, 2, 3, 0, 0), vector_dst);

            vector_dst = ComplexVector.Zero(vector_src.Dim);
            vector_dst[1..4] = vector_src[1..4];
            Assert.AreEqual(new ComplexVector(0, 2, 3, 4, 0), vector_dst);

            vector_dst = ComplexVector.Zero(vector_src.Dim);
            vector_dst[1..^1] = vector_src[1..^1];
            Assert.AreEqual(new ComplexVector(0, 2, 3, 4, 0), vector_dst);

            vector_dst = ComplexVector.Zero(vector_src.Dim);
            vector_dst[0..^2] = vector_src[1..^1];
            Assert.AreEqual(new ComplexVector(2, 3, 4, 0, 0), vector_dst);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                vector_dst = ComplexVector.Zero(vector_src.Dim);
                vector_dst[0..^2] = vector_src[1..^2];
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                vector_dst = ComplexVector.Zero(vector_src.Dim);
                vector_dst[0..^2] = vector_src[1..];
            });
        }

        [TestMethod()]
        public void ArrayIndexerTest() {
            ComplexVector v = new(1, 2, 3, 4, 5, 6, 7);

            Assert.AreEqual(new ComplexVector(5, 2, 3, 7), v[[4, 1, 2, 6]]);

            v[[2, 1, 3]] = new(4, 0, 8);

            Assert.AreEqual(4, v[2]);
            Assert.AreEqual(0, v[1]);
            Assert.AreEqual(8, v[3]);
        }
    }
}