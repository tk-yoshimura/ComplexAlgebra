using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexMatrix {
        public static ComplexMatrix Grid((int min, int max) real, (int min, int max) imag) {
            if (unchecked(real.max - real.min) < 0 || real.max - real.min >= int.MaxValue) {
                throw new ArgumentException("invalid range", nameof(real));
            }

            if (unchecked(imag.max - imag.min) < 0 || imag.max - imag.min >= int.MaxValue) {
                throw new ArgumentException("invalid range", nameof(imag));
            }

            int rows = real.max - real.min + 1;
            int cols = imag.max - imag.min + 1;

            Complex[,] e = new Complex[rows, cols];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    e[i, j] = (real.min + i, imag.min + j);
                }
            }

            return new ComplexMatrix(e, cloning: false);
        }
    }
}
