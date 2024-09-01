using DoubleDouble;
using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexMatrix {
        public static ComplexMatrix operator +(ComplexMatrix matrix) {
            return matrix.Copy();
        }

        public static ComplexMatrix operator -(ComplexMatrix matrix) {
            Complex[,] ret = new Complex[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = -e[i, j];
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        public static ComplexMatrix operator +(ComplexMatrix matrix1, ComplexMatrix matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Complex[,] ret = new Complex[matrix1.Rows, matrix1.Columns], e1 = matrix1.e, e2 = matrix2.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = e1[i, j] + e2[i, j];
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        public static ComplexMatrix operator -(ComplexMatrix matrix1, ComplexMatrix matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Complex[,] ret = new Complex[matrix1.Rows, matrix1.Columns], e1 = matrix1.e, e2 = matrix2.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = e1[i, j] - e2[i, j];
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        public static ComplexMatrix ElementwiseMul(ComplexMatrix matrix1, ComplexMatrix matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Complex[,] ret = new Complex[matrix1.Rows, matrix1.Columns], e1 = matrix1.e, e2 = matrix2.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = e1[i, j] * e2[i, j];
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        public static ComplexMatrix ElementwiseDiv(ComplexMatrix matrix1, ComplexMatrix matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Complex[,] ret = new Complex[matrix1.Rows, matrix1.Columns], e1 = matrix1.e, e2 = matrix2.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = e1[i, j] / e2[i, j];
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        public static ComplexMatrix operator *(ComplexMatrix matrix1, ComplexMatrix matrix2) {
            if (matrix1.Columns != matrix2.Rows) {
                throw new ArgumentException($"mismatch {nameof(matrix1.Columns)} {nameof(matrix2.Rows)}", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Complex[,] ret = new Complex[matrix1.Rows, matrix2.Columns], e1 = matrix1.e, e2 = matrix2.T.e;

            for (int i = 0, c = matrix1.Columns; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    Complex s = Complex.Zero;

                    for (int k = 0; k < c; k++) {
                        s += e1[i, k] * e2[j, k];
                    }

                    ret[i, j] = s;
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        public static ComplexVector operator *(ComplexMatrix matrix, ComplexVector vector) {
            if (matrix.Columns != vector.Dim) {
                throw new ArgumentException($"mismatch {nameof(matrix.Columns)} {nameof(vector.Dim)}", $"{nameof(matrix)},{nameof(vector)}");
            }

            Complex[] ret = new Complex[matrix.Rows], v = vector.v;
            Complex[,] e = matrix.e;

            for (int i = 0; i < matrix.Rows; i++) {
                Complex s = Complex.Zero;

                for (int j = 0; j < matrix.Columns; j++) {
                    s += e[i, j] * v[j];
                }

                ret[i] = s;
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexVector operator *(ComplexVector vector, ComplexMatrix matrix) {
            if (vector.Dim != matrix.Rows) {
                throw new ArgumentException($"mismatch {nameof(vector.Dim)} {nameof(matrix.Rows)}", $"{nameof(vector)},{nameof(matrix)}");
            }

            Complex[] ret = new Complex[matrix.Columns], v = vector.v;
            Complex[,] e = matrix.T.e;

            for (int j = 0; j < matrix.Columns; j++) {
                Complex s = Complex.Zero;

                for (int i = 0; i < matrix.Rows; i++) {
                    s += v[i] * e[j, i];
                }

                ret[j] = s;
            }

            return new ComplexVector(ret, cloning: false);
        }

        public static ComplexMatrix operator +(ddouble r, ComplexMatrix matrix) {
            Complex[,] ret = new Complex[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r + e[i, j];
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        public static ComplexMatrix operator +(Complex r, ComplexMatrix matrix) {
            Complex[,] ret = new Complex[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r + e[i, j];
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        public static ComplexMatrix operator +(ComplexMatrix matrix, ddouble r) {
            return r + matrix;
        }

        public static ComplexMatrix operator +(ComplexMatrix matrix, Complex r) {
            return r + matrix;
        }

        public static ComplexMatrix operator -(ddouble r, ComplexMatrix matrix) {
            Complex[,] ret = new Complex[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r - e[i, j];
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        public static ComplexMatrix operator -(Complex r, ComplexMatrix matrix) {
            Complex[,] ret = new Complex[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r - e[i, j];
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        public static ComplexMatrix operator -(ComplexMatrix matrix, ddouble r) {
            return (-r) + matrix;
        }

        public static ComplexMatrix operator -(ComplexMatrix matrix, Complex r) {
            return (-r) + matrix;
        }

        public static ComplexMatrix operator *(ddouble r, ComplexMatrix matrix) {
            Complex[,] ret = new Complex[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r * e[i, j];
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        public static ComplexMatrix operator *(Complex r, ComplexMatrix matrix) {
            Complex[,] ret = new Complex[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r * e[i, j];
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        public static ComplexMatrix operator *(ComplexMatrix matrix, ddouble r) {
            return r * matrix;
        }

        public static ComplexMatrix operator *(ComplexMatrix matrix, Complex r) {
            return r * matrix;
        }

        public static ComplexMatrix operator /(ddouble r, ComplexMatrix matrix) {
            Complex[,] ret = new Complex[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r / e[i, j];
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        public static ComplexMatrix operator /(Complex r, ComplexMatrix matrix) {
            Complex[,] ret = new Complex[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r / e[i, j];
                }
            }

            return new ComplexMatrix(ret, cloning: false);
        }

        public static ComplexMatrix operator /(ComplexMatrix matrix, ddouble r) {
            return (1 / r) * matrix;
        }

        public static ComplexMatrix operator /(ComplexMatrix matrix, Complex r) {
            return (1 / r) * matrix;
        }

        public static ComplexMatrix DiagonalAdd(ComplexMatrix matrix, Complex c) {
            int n = int.Min(matrix.Rows, matrix.Columns);

            ComplexMatrix ret = matrix.Copy();

            for (int i = 0; i < n; i++) {
                ret[i, i] += c;
            }

            return ret;
        }

        public static bool operator ==(ComplexMatrix matrix1, ComplexMatrix matrix2) {
            if (ReferenceEquals(matrix1, matrix2)) {
                return true;
            }
            if (matrix1 is null || matrix2 is null) {
                return false;
            }

            if (matrix1.Shape != matrix2.Shape) {
                return false;
            }

            for (int i = 0; i < matrix1.Rows; i++) {
                for (int j = 0; j < matrix2.Columns; j++) {
                    if (matrix1.e[i, j] != matrix2.e[i, j]) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator !=(ComplexMatrix matrix1, ComplexMatrix matrix2) {
            return !(matrix1 == matrix2);
        }
    }
}
