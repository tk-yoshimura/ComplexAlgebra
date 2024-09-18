using Algebra;
using DoubleDouble;
using DoubleDoubleComplex;
using System.Collections;
using System.Diagnostics;

namespace ComplexAlgebra {
    [DebuggerDisplay("{ToString(),nq}")]
    public partial class ComplexVector :
        ICloneable, IFormattable,
        IEnumerable<(int index, Complex val)>,
        System.Numerics.IAdditionOperators<ComplexVector, ComplexVector, ComplexVector>,
        System.Numerics.ISubtractionOperators<ComplexVector, ComplexVector, ComplexVector>,
        System.Numerics.IMultiplyOperators<ComplexVector, ComplexVector, ComplexVector>,
        System.Numerics.IDivisionOperators<ComplexVector, ComplexVector, ComplexVector>,
        System.Numerics.IUnaryPlusOperators<ComplexVector, ComplexVector>,
        System.Numerics.IUnaryNegationOperators<ComplexVector, ComplexVector> {

        internal readonly Complex[] v;

        internal ComplexVector(Complex[] v, bool cloning) {
            this.v = cloning ? (Complex[])v.Clone() : v;
        }

        protected ComplexVector(int size) {
            this.v = new Complex[size];

            for (int i = 0; i < v.Length; i++) {
                this.v[i] = Complex.Zero;
            }
        }

        public ComplexVector(params Complex[] v) : this(v, cloning: true) { }

        public ComplexVector(IEnumerable<Complex> v) {
            this.v = v.ToArray();
        }

        public ComplexVector(IReadOnlyCollection<Complex> v) {
            this.v = [.. v];
        }

        public ComplexVector(Vector real, Vector imag) {
            if (real.Dim != imag.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(real)}, {nameof(imag)}");
            }

            this.v = new Complex[real.Dim];

            for (int i = 0; i < v.Length; i++) {
                this.v[i] = (real[i], imag[i]);
            }
        }

        public ComplexVector(Matrix matrix) {
            if (matrix.Columns != 2) {
                throw new ArgumentException("invalid columns", nameof(matrix));
            }

            this.v = new Complex[matrix.Rows];

            for (int i = 0; i < v.Length; i++) {
                this.v[i] = (matrix[i, 0], matrix[i, 1]);
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex X {
            get => v[0];
            set => v[0] = value;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex Y {
            get => v[1];
            set => v[1] = value;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex Z {
            get => v[2];
            set => v[2] = value;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex W {
            get => v[3];
            set => v[3] = value;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Vector R {
            get {
                return new(v.Select(c => c.R));
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Vector I {
            get {
                return new(v.Select(c => c.R));
            }
        }

        public int Dim => v.Length;

        public static implicit operator Complex[](ComplexVector vector) {
            return (Complex[])vector.v.Clone();
        }

        public static implicit operator ComplexVector(Complex[] arr) {
            return new ComplexVector(arr);
        }

        public static implicit operator ComplexVector(Vector vector) {
            Complex[] ret = new Complex[vector.Dim];

            for (int i = 0; i < vector.Dim; i++) {
                ret[i] = vector[i];
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexVector Conj => Conjugate(this);

        public static ComplexVector Conjugate(ComplexVector v) {
            Complex[] ret = new Complex[v.Dim], e = v.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = e[i].Conj;
            }

            return new ComplexVector(ret, cloning: false);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexMatrix Horizontal {
            get {
                ComplexMatrix ret = ComplexMatrix.Zero(1, Dim);

                for (int i = 0; i < Dim; i++) {
                    ret.e[0, i] = v[i];
                }

                return ret;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexMatrix Vertical {
            get {
                ComplexMatrix ret = ComplexMatrix.Zero(Dim, 1);

                for (int i = 0; i < Dim; i++) {
                    ret.e[i, 0] = v[i];
                }

                return ret;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexVector Normal => this / Norm;

        public static ddouble Distance(ComplexVector vector1, ComplexVector vector2) {
            return (vector1 - vector2).Norm;
        }

        public static ddouble SquareDistance(ComplexVector vector1, ComplexVector vector2) {
            return (vector1 - vector2).SquareNorm;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble Norm =>
            IsFinite(this)
            ? (IsZero(this) ? ddouble.Zero : ddouble.Ldexp(ddouble.Sqrt(ScaleB(this, -MaxExponent).SquareNorm), MaxExponent))
            : !IsValid(this) ? ddouble.NaN : ddouble.PositiveInfinity;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble SquareNorm {
            get {
                ddouble sum_sq = ddouble.Zero;

                foreach (var vi in v) {
                    sum_sq += vi.Norm;
                }

                return sum_sq;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex Sum {
            get {
                Complex sum = Complex.Zero;

                for (int i = 0; i < Dim; i++) {
                    sum += v[i];
                }

                return sum;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex Mean => Sum / Dim;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int MaxExponent {
            get {
                int max_exponent = int.MinValue + 1; // abs(int.minvalue) throw arithmetic exception

                for (int i = 0; i < Dim; i++) {
                    max_exponent = Math.Max(Complex.ILogB(v[i]), max_exponent);
                }

                return max_exponent;
            }
        }

        public static ComplexVector ScaleB(ComplexVector vector, int n) {
            ComplexVector ret = vector.Copy();

            for (int i = 0; i < ret.Dim; i++) {
                ret.v[i] = (ddouble.Ldexp(ret.v[i].R, n), ddouble.Ldexp(ret.v[i].I, n));
            }

            return ret;
        }

        public static ComplexVector Zero(int size) {
            return new ComplexVector(size);
        }

        public static ComplexVector Fill(int size, Complex value) {
            Complex[] v = new Complex[size];

            for (int i = 0; i < v.Length; i++) {
                v[i] = value;
            }

            return new ComplexVector(v, cloning: false);
        }

        public static ComplexVector Invalid(int size) {
            return Fill(size, value: ddouble.NaN);
        }

        public static bool IsZero(ComplexVector vector) {
            for (int i = 0; i < vector.Dim; i++) {
                if (!Complex.IsZero(vector.v[i])) {
                    return false;
                }
            }

            return true;
        }

        public static bool IsFinite(ComplexVector vector) {
            for (int i = 0; i < vector.Dim; i++) {
                if (!Complex.IsFinite(vector.v[i])) {
                    return false;
                }
            }

            return true;
        }

        public static bool IsInfinity(ComplexVector vector) {
            if (!IsValid(vector)) {
                return false;
            }

            for (int i = 0; i < vector.Dim; i++) {
                if (ddouble.IsInfinity(vector.v[i].R)) {
                    return true;
                }
                if (ddouble.IsInfinity(vector.v[i].I)) {
                    return true;
                }
            }

            return false;
        }

        public static bool IsValid(ComplexVector vector) {
            if (vector.Dim < 1) {
                return false;
            }

            for (int i = 0; i < vector.Dim; i++) {
                if (ddouble.IsNaN(vector.v[i].R)) {
                    return false;
                }
                if (ddouble.IsNaN(vector.v[i].I)) {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj) {
            return (obj is not null) && obj is ComplexVector vector && vector == this;
        }

        public override int GetHashCode() {
            return Dim > 0 ? v[0].GetHashCode() : 0;
        }

        public object Clone() {
            return new ComplexVector(v);
        }

        public ComplexVector Copy() {
            return new ComplexVector(v);
        }

        public IEnumerator<(int index, Complex val)> GetEnumerator() {
            for (int i = 0; i < Dim; i++) {
                yield return (i, v[i]);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
