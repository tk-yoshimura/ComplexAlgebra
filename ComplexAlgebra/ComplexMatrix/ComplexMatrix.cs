using Algebra;
using DoubleDouble;
using DoubleDoubleComplex;
using System.Collections;
using System.Diagnostics;

namespace ComplexAlgebra {
    [DebuggerDisplay("{ToString(),nq}")]
    public partial class ComplexMatrix :
        ICloneable, IFormattable,
        IEnumerable<(int row_index, int column_index, Complex val)>,
        System.Numerics.IAdditionOperators<ComplexMatrix, ComplexMatrix, ComplexMatrix>,
        System.Numerics.ISubtractionOperators<ComplexMatrix, ComplexMatrix, ComplexMatrix>,
        System.Numerics.IMultiplyOperators<ComplexMatrix, ComplexMatrix, ComplexMatrix>,
        System.Numerics.IUnaryPlusOperators<ComplexMatrix, ComplexMatrix>,
        System.Numerics.IUnaryNegationOperators<ComplexMatrix, ComplexMatrix> {

        internal readonly Complex[,] e;

        protected ComplexMatrix(Complex[,] m, bool cloning) {
            this.e = cloning ? (Complex[,])m.Clone() : m;
        }

        protected ComplexMatrix(int rows, int columns) {
            if (rows <= 0 || columns <= 0) {
                throw new ArgumentOutOfRangeException($"{nameof(rows)},{nameof(columns)}");
            }

            this.e = new Complex[rows, columns];

            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    e[i, j] = Complex.Zero;
                }
            }
        }

        public ComplexMatrix(Complex[,] m) : this(m, cloning: true) { }

        public ComplexMatrix(Matrix real, Matrix imag) {
            if (real.Shape != imag.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(real)}, {nameof(imag)}");
            }

            this.e = new Complex[real.Rows, real.Columns];

            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    e[i, j] = (real[i, j], imag[i, j]);
                }
            }
        }

        public int Rows => e.GetLength(0);

        public int Columns => e.GetLength(1);

        public (int rows, int columns) Shape => (e.GetLength(0), e.GetLength(1));

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int Size {
            get {
                if (!IsSquare(this)) {
                    throw new ArithmeticException("not square matrix");
                }

                return Rows;
            }
        }

        public static implicit operator Complex[,](ComplexMatrix matrix) {
            return (Complex[,])matrix.e.Clone();
        }

        public static implicit operator ComplexMatrix(Complex[,] arr) {
            return new ComplexMatrix(arr);
        }

        public static implicit operator ComplexMatrix(Matrix matrix) {
            Complex[,] ret = new Complex[matrix.Rows, matrix.Columns];

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    ret[i, j] = matrix[i, j];
                }
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Matrix R {
            get {
                ddouble[,] ret = new ddouble[Rows, Columns];

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        ret[i, j] = e[i, j].R;
                    }
                }

                return ret;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Matrix I {
            get {
                ddouble[,] ret = new ddouble[Rows, Columns];

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        ret[i, j] = e[i, j].I;
                    }
                }

                return ret;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexMatrix Conj => Conjugate(this);

        public static ComplexMatrix Conjugate(ComplexMatrix m) {
            Complex[,] ret = new Complex[m.Rows, m.Columns], e = m.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = e[i, j].Conj;
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexMatrix T => Transpose(this);

        public static ComplexMatrix Transpose(ComplexMatrix m) {
            ComplexMatrix ret = new(m.Columns, m.Rows);

            for (int i = 0; i < m.Rows; i++) {
                for (int j = 0; j < m.Columns; j++) {
                    ret.e[j, i] = m.e[i, j];
                }
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexMatrix H => Adjoint(this);

        public static ComplexMatrix Adjoint(ComplexMatrix m) {
            ComplexMatrix ret = new(m.Columns, m.Rows);

            for (int i = 0; i < m.Rows; i++) {
                for (int j = 0; j < m.Columns; j++) {
                    ret.e[j, i] = m.e[i, j].Conj;
                }
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexMatrix Inverse => Invert(this);

        public static ComplexMatrix Invert(ComplexMatrix m) {
            if (IsZero(m) || !IsFinite(m)) {
                return Invalid(m.Columns, m.Rows);
            }
            if (m.Rows == m.Columns) {
                if (m.Rows == 1) {
                    return new ComplexMatrix(new Complex[,] { { 1 / m.e[0, 0] } }, cloning: false);
                }
                if (m.Rows == 2) {
                    return Invert2x2(m);
                }
                if (m.Rows == 3) {
                    return Invert3x3(m);
                }

                return GaussianEliminate(m);
            }
            else if (m.Rows < m.Columns) {
                ComplexMatrix mh = m.H, mr = m * mh;
                return mh * InversePositiveHermitian(mr, enable_check_hermitian: false);
            }
            else {
                ComplexMatrix mh = m.H, mr = mh * m;
                return InversePositiveHermitian(mr, enable_check_hermitian: false) * mh;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble Norm =>
            IsFinite(this) ? (IsZero(this) ? ddouble.Zero : ddouble.Ldexp(ddouble.Sqrt(ScaleB(this, -MaxExponent).SquareNorm), MaxExponent))
            : !IsValid(this) ? ddouble.NaN : ddouble.PositiveInfinity;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble SquareNorm {
            get {
                ddouble sum_sq = ddouble.Zero;

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        sum_sq += e[i, j].Norm;
                    }
                }

                return sum_sq;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex Sum {
            get {
                Complex sum = Complex.Zero;

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        sum += e[i, j];
                    }
                }

                return sum;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex Mean => Sum / checked(Rows * Columns);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int MaxExponent {
            get {
                int max_exponent = int.MinValue + 1; // abs(int.minvalue) throw arithmetic exception

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        max_exponent = int.Max(Complex.ILogB(e[i, j]), max_exponent);
                    }
                }

                return max_exponent;
            }
        }

        public static ComplexMatrix ScaleB(ComplexMatrix matrix, int n) {
            ComplexMatrix ret = matrix.Copy();

            for (int i = 0; i < ret.Rows; i++) {
                for (int j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = Complex.Ldexp(ret.e[i, j], n);
                }
            }

            return ret;
        }

        public ComplexVector Horizontal(int row_index) {
            ComplexVector ret = ComplexVector.Zero(Columns);

            for (int i = 0; i < Columns; i++) {
                ret.v[i] = e[row_index, i];
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexVector[] Horizontals => (new ComplexVector[Rows]).Select((_, idx) => Horizontal(idx)).ToArray();

        public ComplexVector Vertical(int column_index) {
            ComplexVector ret = ComplexVector.Zero(Rows);

            for (int i = 0; i < Rows; i++) {
                ret.v[i] = e[i, column_index];
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComplexVector[] Verticals => (new ComplexVector[Columns]).Select((_, idx) => Vertical(idx)).ToArray();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex[] Diagonals {
            get {
                if (!IsSquare(this)) {
                    throw new ArithmeticException("not square matrix");
                }

                Complex[] diagonals = new Complex[Size];

                for (int i = 0; i < Size; i++) {
                    diagonals[i] = e[i, i];
                }

                return diagonals;
            }
        }

        public static ComplexMatrix FromDiagonals(Complex[] vs) {
            Complex[,] v = new Complex[vs.Length, vs.Length];

            for (int i = 0; i < vs.Length; i++) {
                for (int j = 0; j < vs.Length; j++) {
                    v[i, j] = Complex.Zero;
                }

                v[i, i] = vs[i];
            }

            return new ComplexMatrix(v, cloning: false);
        }

        public static ComplexVector Flatten(ComplexMatrix matrix) {
            Complex[] v = new Complex[matrix.Rows * matrix.Columns];

            for (int i = 0, idx = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++, idx++) {
                    v[idx] = matrix.e[i, j];
                }
            }

            return new ComplexVector(v, cloning: false);
        }

        public static ComplexMatrix Zero(int rows, int columns) {
            return new ComplexMatrix(rows, columns);
        }

        public static ComplexMatrix Zero(int size) {
            return new ComplexMatrix(size, size);
        }

        public static ComplexMatrix Fill(int rows, int columns, Complex value) {
            Complex[,] v = new Complex[rows, columns];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    v[i, j] = value;
                }
            }

            return new ComplexMatrix(v, cloning: false);
        }

        public static ComplexMatrix Fill(int size, ddouble value) {
            return Fill(size, size, value);
        }

        public static ComplexMatrix Identity(int size) {
            ComplexMatrix ret = new(size, size);

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    ret.e[i, j] = (i == j) ? 1 : 0;
                }
            }

            return ret;
        }

        public static ComplexMatrix Invalid(int rows, int columns) {
            return Fill(rows, columns, value: ddouble.NaN);
        }

        public static ComplexMatrix Invalid(int size) {
            return Fill(size, value: ddouble.NaN);
        }

        public static bool IsSquare(ComplexMatrix matrix) {
            return matrix.Rows == matrix.Columns;
        }

        public static bool IsDiagonal(ComplexMatrix matrix) {
            if (!IsSquare(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (i != j && matrix.e[i, j] != 0) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsZero(ComplexMatrix matrix) {
            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (!Complex.IsZero(matrix.e[i, j])) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsIdentity(ComplexMatrix matrix) {
            if (!IsSquare(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (i == j) {
                        if (matrix.e[i, j] != ddouble.One) {
                            return false;
                        }
                    }
                    else {
                        if (!Complex.IsZero(matrix.e[i, j])) {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public static bool IsSymmetric(ComplexMatrix matrix) {
            if (!IsSquare(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = i + 1; j < matrix.Columns; j++) {
                    if (matrix.e[i, j] != matrix.e[j, i]) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsHermitian(ComplexMatrix matrix) {
            if (!IsSquare(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = i; j < matrix.Columns; j++) {
                    if (matrix.e[i, j] != matrix.e[j, i].Conj) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsFinite(ComplexMatrix matrix) {
            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (!Complex.IsFinite(matrix.e[i, j])) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsInfinity(ComplexMatrix matrix) {
            if (!IsValid(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (ddouble.IsInfinity(matrix.e[i, j].R)) {
                        return true;
                    }
                    if (ddouble.IsInfinity(matrix.e[i, j].I)) {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsValid(ComplexMatrix matrix) {
            if (matrix.Rows < 1 || matrix.Columns < 1) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (ddouble.IsNaN(matrix.e[i, j].R)) {
                        return false;
                    }
                    if (ddouble.IsNaN(matrix.e[i, j].I)) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsRegular(ComplexMatrix matrix) {
            return IsFinite(Invert(matrix));
        }

        public override bool Equals(object obj) {
            return (obj is not null) && obj is ComplexMatrix matrix && matrix == this;
        }

        public override int GetHashCode() {
            return e[0, 0].GetHashCode();
        }

        public object Clone() {
            return new ComplexMatrix(e);
        }

        public ComplexMatrix Copy() {
            return new ComplexMatrix(e);
        }

        public IEnumerator<(int row_index, int column_index, Complex val)> GetEnumerator() {
            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    yield return (i, j, e[i, j]);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
