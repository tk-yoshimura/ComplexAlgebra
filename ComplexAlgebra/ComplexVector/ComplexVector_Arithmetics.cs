using DoubleDouble;
using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexVector {
        public static ComplexVector operator +(ComplexVector vector) {
            return (ComplexVector)vector.Clone();
        }

        public static ComplexVector operator -(ComplexVector vector) {
            Complex[] ret = new Complex[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = -v[i];
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexVector operator +(ComplexVector vector1, ComplexVector vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            Complex[] ret = new Complex[vector1.Dim], v1 = vector1.v, v2 = vector2.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = v1[i] + v2[i];
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexVector operator -(ComplexVector vector1, ComplexVector vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            Complex[] ret = new Complex[vector1.Dim], v1 = vector1.v, v2 = vector2.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = v1[i] - v2[i];
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexVector operator *(ComplexVector vector1, ComplexVector vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            Complex[] ret = new Complex[vector1.Dim], v1 = vector1.v, v2 = vector2.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = v1[i] * v2[i];
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexVector operator /(ComplexVector vector1, ComplexVector vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            Complex[] ret = new Complex[vector1.Dim], v1 = vector1.v, v2 = vector2.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = v1[i] / v2[i];
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexVector operator +(ddouble r, ComplexVector vector) {
            Complex[] ret = new Complex[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r + v[i];
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexVector operator +(Complex r, ComplexVector vector) {
            Complex[] ret = new Complex[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r + v[i];
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexVector operator +(ComplexVector vector, ddouble r) {
            return r + vector;
        }

        public static ComplexVector operator +(ComplexVector vector, Complex r) {
            return r + vector;
        }

        public static ComplexVector operator -(ddouble r, ComplexVector vector) {
            Complex[] ret = new Complex[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r - v[i];
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexVector operator -(Complex r, ComplexVector vector) {
            Complex[] ret = new Complex[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r - v[i];
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexVector operator -(ComplexVector vector, ddouble r) {
            return (-r) + vector;
        }

        public static ComplexVector operator -(ComplexVector vector, Complex r) {
            return (-r) + vector;
        }

        public static ComplexVector operator *(ddouble r, ComplexVector vector) {
            Complex[] ret = new Complex[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r * v[i];
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexVector operator *(Complex r, ComplexVector vector) {
            Complex[] ret = new Complex[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r * v[i];
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexVector operator *(ComplexVector vector, ddouble r) {
            return r * vector;
        }

        public static ComplexVector operator *(ComplexVector vector, Complex r) {
            return r * vector;
        }

        public static ComplexVector operator /(ddouble r, ComplexVector vector) {
            Complex[] ret = new Complex[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r / v[i];
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexVector operator /(Complex r, ComplexVector vector) {
            Complex[] ret = new Complex[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r / v[i];
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexVector operator /(ComplexVector vector, ddouble r) {
            return (1 / r) * vector;
        }

        public static ComplexVector operator /(ComplexVector vector, Complex r) {
            return (1 / r) * vector;
        }

        public static Complex Dot(ComplexVector vector1, ComplexVector vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            Complex sum = Complex.Zero;

            for (int i = 0, dim = vector1.Dim; i < dim; i++) {
                sum += vector1.v[i] * Complex.Conjugate(vector2.v[i]);
            }

            return sum;
        }

        public static Complex Polynomial(Complex x, ComplexVector coef) {
            if (coef.Dim < 1) {
                return Complex.Zero;
            }

            Complex y = coef[^1];

            for (int i = coef.Dim - 2; i >= 0; i--) {
                y = coef[i] + x * y;
            }

            return y;
        }

        public static ComplexVector Polynomial(ComplexVector x, ComplexVector coef) {
            if (coef.Dim < 1) {
                return Zero(x.Dim);
            }

            ComplexVector y = Fill(x.Dim, coef[^1]);

            for (int i = coef.Dim - 2; i >= 0; i--) {
                Complex c = coef[i];

                for (int j = 0, n = x.Dim; j < n; j++) {
                    y[j] = c + x[j] * y[j];
                }
            }

            return y;
        }

        public static bool operator ==(ComplexVector vector1, ComplexVector vector2) {
            if (ReferenceEquals(vector1, vector2)) {
                return true;
            }
            if (vector1 is null || vector2 is null) {
                return false;
            }

            return vector1.v.SequenceEqual(vector2.v);
        }

        public static bool operator !=(ComplexVector vector1, ComplexVector vector2) {
            return !(vector1 == vector2);
        }
    }
}
