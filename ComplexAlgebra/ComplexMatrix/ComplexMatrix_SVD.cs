using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexMatrix {
        public static (ComplexMatrix u, ComplexVector s, ComplexMatrix v) SVD(ComplexMatrix m) {
            if (m.Rows == m.Columns) {
                (ComplexVector eigen_vals, ComplexVector[] eigen_vecs) = EigenValueVectors(m * m.H);

                ComplexMatrix u = HConcat(eigen_vecs);
                ComplexVector s = (Complex.Sqrt, eigen_vals);
                ComplexMatrix v = (FromDiagonals(1 / s) * u.H * m).H;

                return (u, s, v);
            }

            if (m.Rows > m.Columns) {
                (ComplexVector eigen_vals, ComplexVector[] eigen_vecs) = EigenValueVectors(m * m.H);

                ComplexMatrix u = HConcat(eigen_vecs);
                ComplexVector s = (Complex.Sqrt, eigen_vals);
                ComplexMatrix v = (FromDiagonals(1 / s) * u.H * m).H;

                s = s[..m.Columns];
                v = v[.., ..m.Columns];

                return (u, s, v);
            }
            else {
                (ComplexVector eigen_vals, ComplexVector[] eigen_vecs) = EigenValueVectors(m.H * m);

                ComplexMatrix v = HConcat(eigen_vecs);
                ComplexVector s = (Complex.Sqrt, eigen_vals);
                ComplexMatrix u = m * v * FromDiagonals(1 / s);

                s = s[..m.Rows];
                u = u[.., ..m.Rows];

                return (u, s, v);
            }
        }
    }
}
