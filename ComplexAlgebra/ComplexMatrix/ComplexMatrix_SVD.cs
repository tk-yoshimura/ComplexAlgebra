using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexMatrix {
        public static (ComplexMatrix u, ComplexVector s, ComplexMatrix v) SVD(ComplexMatrix m) {
            if (m.Rows < 1 || m.Columns < 1) {
                throw new ArgumentException("empty matrix", nameof(m));
            }

            if (m.Rows == m.Columns) {
                (ComplexVector eigen_vals, ComplexVector[] eigen_vecs) = EigenValueVectors(m.H * m);

                ComplexMatrix v = HConcat(eigen_vecs);
                ComplexVector s = (Complex.Sqrt, eigen_vals);
                ComplexMatrix u = m * v;

                for (int i = 0; i < s.Dim; i++) {
                    u[.., i] /= s[i];
                }

                return (u, s, v);
            }

            if (m.Rows > m.Columns) {
                (ComplexVector eigen_vals, ComplexVector[] eigen_vecs) = EigenValueVectors(m.H * m);

                ComplexMatrix v = HConcat(eigen_vecs);
                ComplexVector s = (Complex.Sqrt, eigen_vals);
                ComplexMatrix l = m * v;

                for (int i = 0; i < s.Dim; i++) {
                    l[.., i] /= s[i];
                }

                List<ComplexVector> ls = [.. l.Verticals];
                GramSchmidtMethod(ls);

                ComplexMatrix u = HConcat(ls.ToArray());

                return (u, s, v);
            }
            else {
                (ComplexVector eigen_vals, ComplexVector[] eigen_vecs) = EigenValueVectors(m * m.H);

                ComplexMatrix u = HConcat(eigen_vecs);
                ComplexVector s = (Complex.Sqrt, eigen_vals);
                ComplexMatrix r = u.H * m;

                for (int i = 0; i < s.Dim; i++) {
                    r[i, ..] /= s[i];
                }

                List<ComplexVector> rs = [.. r.Horizontals];
                GramSchmidtMethod(rs);

                ComplexMatrix v = HConcat(rs.ToArray()).Conj;

                return (u, s, v);
            }
        }

        private static void GramSchmidtMethod(List<ComplexVector> vs) {
            int n = vs[0].Dim;

            ComplexVector g = ComplexVector.Fill(n, 1);
            for (int i = 0; i < vs.Count; i++) {
                g += vs[i];
            }

            int g_sft = 0;

            while (vs.Count < n) {
                ComplexVector v = g;
                bool is_success = true;

                for (int i = 0; i < vs.Count; i++) {
                    Complex dot = ComplexVector.Dot(vs[i], g);

                    // rare case: a vector existed that was orthogonal to the selected vector
                    if (Complex.IsZero(dot) && !ComplexVector.IsZero(vs[i])) {
                        g[g_sft % n] += 1;
                        g_sft++;
                        is_success = false;
                    }

                    v -= vs[i] * dot / vs[i].SquareNorm;
                }

                if (!is_success) {
                    continue;
                }

                v = v.Normal;
                g += v;

                vs.Add(v);
            }
        }
    }
}
