using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexMatrix {
        public static implicit operator ComplexMatrix((Func<Complex, Complex> func, ComplexMatrix arg) sel) {
            return Func(sel.func, sel.arg);
        }

        public static implicit operator ComplexMatrix((Func<Complex, Complex, Complex> func, (ComplexMatrix matrix1, ComplexMatrix matrix2) args) sel) {
            return Func(sel.func, sel.args.matrix1, sel.args.matrix2);
        }

        public static implicit operator ComplexMatrix((Func<Complex, Complex, Complex, Complex> func, (ComplexMatrix matrix1, ComplexMatrix matrix2, ComplexMatrix matrix3) args) sel) {
            return Func(sel.func, sel.args.matrix1, sel.args.matrix2, sel.args.matrix3);
        }

        public static implicit operator ComplexMatrix((Func<Complex, Complex, Complex, Complex, Complex> func, (ComplexMatrix matrix1, ComplexMatrix matrix2, ComplexMatrix matrix3, ComplexMatrix matrix4) args) sel) {
            return Func(sel.func, sel.args.matrix1, sel.args.matrix2, sel.args.matrix3, sel.args.matrix4);
        }

        public static implicit operator ComplexMatrix((Func<Complex, Complex, Complex> func, ComplexVector vector_row, ComplexVector vector_column) sel) {
            return Map(sel.func, sel.vector_row, sel.vector_column);
        }

        public static ComplexMatrix Func(Func<Complex, Complex> f, ComplexMatrix matrix) {
            Complex[,] x = matrix.e, v = new Complex[matrix.Rows, matrix.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j]);
                }
            }

            return new ComplexMatrix(v, cloning: false);
        }

        public static ComplexMatrix Func(Func<Complex, Complex, Complex> f, ComplexMatrix matrix1, ComplexMatrix matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Complex[,] x = matrix1.e, y = matrix2.e, v = new Complex[matrix1.Rows, matrix1.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j], y[i, j]);
                }
            }

            return new ComplexMatrix(v, cloning: false);
        }

        public static ComplexMatrix Func(Func<Complex, Complex, Complex, Complex> f, ComplexMatrix matrix1, ComplexMatrix matrix2, ComplexMatrix matrix3) {
            if (matrix1.Shape != matrix2.Shape || matrix1.Shape != matrix3.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)},{nameof(matrix3)}");
            }

            Complex[,] x = matrix1.e, y = matrix2.e, z = matrix3.e, v = new Complex[matrix1.Rows, matrix1.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j], y[i, j], z[i, j]);
                }
            }

            return new ComplexMatrix(v, cloning: false);
        }

        public static ComplexMatrix Func(Func<Complex, Complex, Complex, Complex, Complex> f, ComplexMatrix matrix1, ComplexMatrix matrix2, ComplexMatrix matrix3, ComplexMatrix matrix4) {
            if (matrix1.Shape != matrix2.Shape || matrix1.Shape != matrix3.Shape || matrix1.Shape != matrix4.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)},{nameof(matrix3)},{nameof(matrix4)}");
            }

            Complex[,] x = matrix1.e, y = matrix2.e, z = matrix3.e, w = matrix4.e, v = new Complex[matrix1.Rows, matrix1.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j], y[i, j], z[i, j], w[i, j]);
                }
            }

            return new ComplexMatrix(v, cloning: false);
        }

        public static ComplexMatrix Map(Func<Complex, Complex, Complex> f, ComplexVector vector_row, ComplexVector vector_column) {
            Complex[] row = vector_row.v, col = vector_column.v;
            Complex[,] v = new Complex[row.Length, col.Length];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(row[i], col[j]);
                }
            }

            return new ComplexMatrix(v, cloning: false);
        }
    }
}
