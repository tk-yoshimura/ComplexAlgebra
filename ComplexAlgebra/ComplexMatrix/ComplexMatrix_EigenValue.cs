using Algebra;
using DoubleDouble;
using DoubleDoubleComplex;
using System.Diagnostics;

namespace ComplexAlgebra {
    public partial class ComplexMatrix {
        public static Complex[] EigenValues(ComplexMatrix m, int precision_level = -1) {
            if (!IsSquare(m) || m.Size < 1) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            if (m.Size <= 1) {
                return [m[0, 0]];
            }
            if (m.Size == 2) {
                return SortEigenByNorm(EigenValues2x2(m));
            }

            precision_level = precision_level >= 0 ? precision_level : 32 * m.Size;

            int n = m.Size, notconverged = n;
            int exponent = m.MaxExponent;
            (ComplexMatrix u, _, _) = PermutateDiagonal(ScaleB(m, -exponent));

            ComplexVector eigen_values = ComplexVector.Fill(n, 1);
            ComplexVector eigen_values_prev = eigen_values.Copy();

            Vector eigen_diffnorms = Vector.Fill(n, ddouble.PositiveInfinity);
            Vector eigen_diffnorms_prev = eigen_diffnorms.Copy();

            ComplexMatrix d = u;

            for (int iter_qr = 0; iter_qr <= precision_level; iter_qr++) {
                if (d.Size > 2) {
                    Complex mu = EigenValues2x2(d[^2.., ^2..])[1];

                    (ComplexMatrix q, ComplexMatrix r) = QR(DiagonalAdd(d, -mu));
                    d = DiagonalAdd(r * q, mu);

                    eigen_values[..d.Size] = d.Diagonals[..d.Size];
                }
                else {
                    eigen_values[..2] = EigenValues2x2(d);
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    ddouble eigen_diffnorm = (eigen_values[i] - eigen_values_prev[i]).Norm;
                    eigen_diffnorms[i] = eigen_diffnorm;
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    if (i >= 2 && iter_qr < precision_level) {
                        if (ddouble.ILogB(eigen_diffnorms[i]) > -98 || eigen_diffnorms_prev[i] > eigen_diffnorms[i]) {
                            break;
                        }
                    }

                    notconverged--;
                }

                if (notconverged <= 0) {
                    break;
                }

                if (d.Size > 2) {
                    ComplexVector lower = d[^1, ..^1];
                    Complex eigen = d[^1, ^1];

                    if (lower.MaxExponent < Complex.ILogB(eigen) - 106L) {
                        d = d[..^1, ..^1];
                    }
                }

                eigen_values_prev[..notconverged] = eigen_values[..notconverged];
                eigen_diffnorms_prev[..notconverged] = eigen_diffnorms[..notconverged];
            }

            eigen_values = ComplexVector.ScaleB(eigen_values, exponent);

            return SortEigenByNorm(eigen_values);
        }

        public static (Complex[] eigen_values, ComplexVector[] eigen_vectors) EigenValueVectors(ComplexMatrix m, int precision_level = -1) {
            if (!IsSquare(m) || m.Size < 1) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            if (m.Size <= 1) {
                return ([m[0, 0]], [new ComplexVector(1)]);
            }
            if (m.Size == 2) {
                return SortEigenByNorm(EigenValueVectors2x2(m));
            }

            precision_level = precision_level >= 0 ? precision_level : 32 * m.Size;

            int n = m.Size, notconverged = n;
            int exponent = m.MaxExponent;
            (ComplexMatrix u, _, int[] perm_indexes) = PermutateDiagonal(ScaleB(m, -exponent));

            ComplexVector eigen_values = ComplexVector.Fill(n, 1);
            ComplexVector eigen_values_prev = eigen_values.Copy();

            Vector eigen_diffnorms = Vector.Fill(n, ddouble.PositiveInfinity);
            Vector eigen_diffnorms_prev = eigen_diffnorms.Copy();

            ComplexVector[] eigen_vectors = Identity(n).Horizontals;

            ComplexMatrix d = u;

            for (int iter_qr = 0; iter_qr <= precision_level; iter_qr++) {
                if (d.Size > 2) {
                    Complex mu = EigenValues2x2(d[^2.., ^2..])[1];

                    (ComplexMatrix q, ComplexMatrix r) = QR(DiagonalAdd(d, -mu));
                    d = DiagonalAdd(r * q, mu);

                    eigen_values[..d.Size] = d.Diagonals[..d.Size];
                }
                else {
                    eigen_values[..2] = EigenValues2x2(d);
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    ddouble eigen_diffnorm = (eigen_values[i] - eigen_values_prev[i]).Norm;
                    eigen_diffnorms[i] = eigen_diffnorm;
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    if (i >= 2 && iter_qr < precision_level) {
                        if (ddouble.ILogB(eigen_diffnorms[i]) > -98 || eigen_diffnorms_prev[i] > eigen_diffnorms[i]) {
                            break;
                        }
                    }

                    Complex eigen_val = eigen_values[i];

                    ComplexVector v = u[.., i], h = u[i, ..];
                    ddouble nondiagonal_absmax = 0d;
                    for (int k = 0; k < v.Dim; k++) {
                        if (k == i) {
                            continue;
                        }

                        nondiagonal_absmax =
                            ddouble.Max(nondiagonal_absmax, ddouble.Abs(v[k].R), ddouble.Abs(h[k].R));
                        nondiagonal_absmax =
                            ddouble.Max(nondiagonal_absmax, ddouble.Abs(v[k].I), ddouble.Abs(h[k].I));
                    }

                    ddouble eps = ddouble.Ldexp(nondiagonal_absmax, -74);

                    ComplexMatrix g = DiagonalAdd(u, -eigen_val + eps).Inverse;

                    ComplexVector x;

                    if (IsFinite(g)) {
                        ddouble norm, norm_prev = ddouble.NaN;
                        x = ComplexVector.Fill(n, 0.125);
                        x[i] = Complex.One;

                        for (int iter_vector = 0; iter_vector < precision_level; iter_vector++) {
                            x = (g * x).Normal;

                            norm = (u * x - eigen_val * x).Norm;

                            if (ddouble.ILogB(norm) < -53 && norm >= norm_prev) {
                                break;
                            }

                            norm_prev = norm;
                        }
                    }
                    else {
                        x = ComplexVector.Zero(n);
                        x[i] = 1d;
                    }

                    eigen_vectors[i] = x[perm_indexes];
                    notconverged--;
                }

                if (notconverged <= 0) {
                    break;
                }

                if (d.Size > 2) {
                    ComplexVector lower = d[^1, ..^1];
                    Complex eigen = d[^1, ^1];

                    if (lower.MaxExponent < Complex.ILogB(eigen) - 106L) {
                        d = d[..^1, ..^1];
                    }
                }

                eigen_values_prev[..notconverged] = eigen_values[..notconverged];
                eigen_diffnorms_prev[..notconverged] = eigen_diffnorms[..notconverged];
            }

            eigen_values = ComplexVector.ScaleB(eigen_values, exponent);

            return SortEigenByNorm((eigen_values, eigen_vectors));
        }

        private static Complex[] EigenValues2x2(ComplexMatrix m) {
            Debug.Assert(m.Size == 2);

            Complex m00 = m[0, 0], m11 = m[1, 1];
            Complex m01 = m[0, 1], m10 = m[1, 0];

            Complex b = m00 + m11, c = m00 - m11;

            Complex d = Complex.Sqrt(c * c + 4d * m01 * m10);

            Complex val0 = (b + d) / 2;
            Complex val1 = (b - d) / 2;

            if ((val0 - m11).Norm >= (val1 - m11).Norm) {
                return [val0, val1];
            }
            else {
                return [val1, val0];
            }
        }

        private static (Complex[] eigen_values, ComplexVector[] eigen_vectors) EigenValueVectors2x2(ComplexMatrix m) {
            Debug.Assert(m.Size == 2);

            Complex m00 = m[0, 0], m11 = m[1, 1];
            Complex m01 = m[0, 1], m10 = m[1, 0];

            long diagonal_scale = long.Max(Complex.ILogB(m00), Complex.ILogB(m11));

            long m10_scale = Complex.ILogB(m10);

            if (diagonal_scale - m10_scale < 106L) {
                Complex b = m00 + m11, c = m00 - m11;

                Complex d = Complex.Sqrt(c * c + 4 * m01 * m10);

                Complex val0 = (b + d) / 2;
                Complex val1 = (b - d) / 2;

                ComplexVector vec0 = new ComplexVector((c + d) / (2 * m10), 1).Normal;
                ComplexVector vec1 = new ComplexVector((c - d) / (2 * m10), 1).Normal;

                if ((val0 - m11).Norm >= (val1 - m11).Norm) {
                    return (new Complex[] { val0, val1 }, new ComplexVector[] { vec0, vec1 });
                }
                else {
                    return (new Complex[] { val1, val0 }, new ComplexVector[] { vec1, vec0 });
                }
            }
            else {
                if (m00 != m11) {
                    ComplexVector vec0 = (1, 0);
                    ComplexVector vec1 = new ComplexVector(m01 / (m11 - m00), 1).Normal;

                    return (new Complex[] { m00, m11 }, new ComplexVector[] { vec0, vec1 });
                }
                else {
                    return (new Complex[] { m00, m11 }, new ComplexVector[] { (1, 0), (0, 1) });
                }
            }
        }

        private static Complex[] SortEigenByNorm(Complex[] eigen_values) {
            Complex[] eigen_values_sorted = [.. eigen_values.OrderByDescending(item => item.Norm)];

            return eigen_values_sorted;
        }

        private static (Complex[] eigen_values, ComplexVector[] eigen_vectors) SortEigenByNorm((Complex[] eigen_values, ComplexVector[] eigen_vectors) eigens) {
            Debug.Assert(eigens.eigen_values.Length == eigens.eigen_vectors.Length);

            IOrderedEnumerable<(Complex val, ComplexVector vec)> eigens_sorted =
                eigens.eigen_values.Zip(eigens.eigen_vectors).OrderByDescending(item => item.First.Norm);

            Complex[] eigen_values_sorted = eigens_sorted.Select(item => item.val).ToArray();
            ComplexVector[] eigen_vectors_sorted = eigens_sorted.Select(item => item.vec).ToArray();

            return (eigen_values_sorted, eigen_vectors_sorted);
        }

        private static (ComplexMatrix matrix, int[] indexes, int[] indexes_invert) PermutateDiagonal(ComplexMatrix m) {
            Debug.Assert(IsSquare(m));

            int n = m.Size;

            Vector rates = Vector.Zero(n);

            for (int i = 0; i < n; i++) {
                Complex diagonal = m[i, i];

                ComplexVector nondigonal = ComplexVector.Concat(m[i, ..i], m[i, (i + 1)..]);

                ddouble nondigonal_norm = nondigonal.Norm;

                ddouble rate = diagonal.Norm / (nondigonal_norm + ddouble.Epsilon);

                rates[i] = rate;
            }

            int[] indexes = rates.Select(item => (item.index, item.val)).OrderBy(item => item.val).Select(item => item.index).ToArray();

            ComplexMatrix m_perm = m[indexes, ..][.., indexes];

            int[] indexes_invert = new int[n];

            for (int i = 0; i < n; i++) {
                indexes_invert[indexes[i]] = i;
            }

            return (m_perm, indexes, indexes_invert);
        }
    }
}
