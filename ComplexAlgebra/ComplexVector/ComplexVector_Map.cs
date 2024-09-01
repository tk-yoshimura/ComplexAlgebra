using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexVector {
        public static implicit operator ComplexVector((Func<Complex, Complex> func, ComplexVector arg) sel) {
            return Func(sel.func, sel.arg);
        }

        public static implicit operator ComplexVector((Func<Complex, Complex, Complex> func, (ComplexVector vector1, ComplexVector vector2) args) sel) {
            return Func(sel.func, sel.args.vector1, sel.args.vector2);
        }

        public static implicit operator ComplexVector((Func<Complex, Complex, Complex, Complex> func, (ComplexVector vector1, ComplexVector vector2, ComplexVector vector3) args) sel) {
            return Func(sel.func, sel.args.vector1, sel.args.vector2, sel.args.vector3);
        }

        public static implicit operator ComplexVector((Func<Complex, Complex, Complex, Complex, Complex> func, (ComplexVector vector1, ComplexVector vector2, ComplexVector vector3, ComplexVector vector4) args) sel) {
            return Func(sel.func, sel.args.vector1, sel.args.vector2, sel.args.vector3, sel.args.vector4);
        }

        public static ComplexVector Func(Func<Complex, Complex> f, ComplexVector vector) {
            Complex[] x = vector.v, v = new Complex[vector.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i]);
            }

            return new ComplexVector(v, cloning: false);
        }

        public static ComplexVector Func(Func<Complex, Complex, Complex> f, ComplexVector vector1, ComplexVector vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            Complex[] x = vector1.v, y = vector2.v, v = new Complex[vector1.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i], y[i]);
            }

            return new ComplexVector(v, cloning: false);
        }

        public static ComplexVector Func(Func<Complex, Complex, Complex, Complex> f, ComplexVector vector1, ComplexVector vector2, ComplexVector vector3) {
            if (vector1.Dim != vector2.Dim || vector1.Dim != vector3.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)},{nameof(vector3)}");
            }

            Complex[] x = vector1.v, y = vector2.v, z = vector3.v, v = new Complex[vector1.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i], y[i], z[i]);
            }

            return new ComplexVector(v, cloning: false);
        }

        public static ComplexVector Func(Func<Complex, Complex, Complex, Complex, Complex> f, ComplexVector vector1, ComplexVector vector2, ComplexVector vector3, ComplexVector vector4) {
            if (vector1.Dim != vector2.Dim || vector1.Dim != vector3.Dim || vector1.Dim != vector4.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)},{nameof(vector3)},{nameof(vector4)}");
            }

            Complex[] x = vector1.v, y = vector2.v, z = vector3.v, w = vector4.v, v = new Complex[vector1.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i], y[i], z[i], w[i]);
            }

            return new ComplexVector(v, cloning: false);
        }
    }
}
