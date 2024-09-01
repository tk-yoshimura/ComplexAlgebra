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

            precision_level = precision_level >= 0 ? precision_level : 4 * m.Size * 8;

            int n = m.Size, notconverged = n;
            int exponent = m.MaxExponent;
            ComplexMatrix u = ScaleB(m, -exponent);

            ComplexVector eigen_values = ComplexVector.Fill(n, 1);
            ComplexVector eigen_values_prev = eigen_values.Copy();

            Vector eigen_diffnorms = Vector.Fill(n, ddouble.PositiveInfinity);
            Vector eigen_diffnorms_prev = eigen_diffnorms.Copy();

            ComplexMatrix d = u;

            for (int iter_qr = 0; iter_qr <= precision_level; iter_qr++) {
                if (d.Size > 2) {
                    Complex[] mu2x2 = EigenValues2x2(d[^2.., ^2..]);
                    Complex d_kk = d[^1, ^1];
                    Complex mu = (d_kk - mu2x2[0]).Norm < (d_kk - mu2x2[1]).Norm
                        ? mu2x2[0] : mu2x2[1];

                    (ComplexMatrix q, ComplexMatrix r) = QR(DiagonalAdd(d, -mu));
                    d = DiagonalAdd(r * q, mu);

                    eigen_values[..d.Size] = d.Diagonals[..d.Size];
                }
                else {
                    eigen_values[..2] = EigenValues(d);
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

                    if (lower.MaxExponent < long.Max(ddouble.ILogB(eigen.R), ddouble.ILogB(eigen.I)) - 106L) {
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

            precision_level = precision_level >= 0 ? precision_level : 4 * m.Size * 8;

            int n = m.Size, notconverged = n;
            int exponent = m.MaxExponent;
            ComplexMatrix u = ScaleB(m, -exponent);
            ddouble eps = ddouble.Ldexp(1, -74);

            ComplexVector eigen_values = ComplexVector.Fill(n, 1);
            ComplexVector eigen_values_prev = eigen_values.Copy();

            Vector eigen_diffnorms = Vector.Fill(n, ddouble.PositiveInfinity);
            Vector eigen_diffnorms_prev = eigen_diffnorms.Copy();

            ComplexVector[] eigen_vectors = Identity(n).Horizontals;

            ComplexMatrix d = u;

            for (int iter_qr = 0; iter_qr <= precision_level; iter_qr++) {
                if (d.Size > 2) {
                    Complex[] mu2x2 = EigenValues2x2(d[^2.., ^2..]);
                    Complex d_kk = d[^1, ^1];
                    Complex mu = (d_kk - mu2x2[0]).Norm < (d_kk - mu2x2[1]).Norm
                        ? mu2x2[0] : mu2x2[1];

                    (ComplexMatrix q, ComplexMatrix r) = QR(DiagonalAdd(d, -mu));
                    d = DiagonalAdd(r * q, mu);

                    eigen_values[..d.Size] = d.Diagonals[..d.Size];
                }
                else {
                    eigen_values[..2] = EigenValues(d);
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
                    ComplexMatrix g = DiagonalAdd(u, -eigen_val + eps).Inverse;

                    ddouble norm, norm_prev = ddouble.NaN;
                    ComplexVector x = ComplexVector.Fill(n, 0.125);
                    x[i] = Complex.One;

                    for (int iter_vector = 0; iter_vector < precision_level; iter_vector++) {
                        x = (g * x).Normal;

                        norm = (u * x - eigen_val * x).Norm;

                        if (ddouble.ILogB(norm) < -53 && norm >= norm_prev) {
                            break;
                        }

                        norm_prev = norm;
                    }

                    eigen_vectors[i] = x;
                    notconverged--;
                }

                if (notconverged <= 0) {
                    break;
                }

                if (d.Size > 2) {
                    ComplexVector lower = d[^1, ..^1];
                    Complex eigen = d[^1, ^1];

                    if (lower.MaxExponent < long.Max(ddouble.ILogB(eigen.R), ddouble.ILogB(eigen.I)) - 106L) {
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

            Complex b = m[0, 0] + m[1, 1], c = m[0, 0] - m[1, 1];

            Complex d = Complex.Sqrt(c * c + 4 * m[0, 1] * m[1, 0]);

            Complex val0 = (b + d) / 2;
            Complex val1 = (b - d) / 2;

            return [val0, val1];
        }

        private static (Complex[] eigen_values, ComplexVector[] eigen_vectors) EigenValueVectors2x2(ComplexMatrix m) {
            Debug.Assert(m.Size == 2);

            long diagonal_scale = long.Max(
                long.Max(ddouble.ILogB(m[0, 0].R), ddouble.ILogB(m[0, 0].I)),
                long.Max(ddouble.ILogB(m[1, 1].R), ddouble.ILogB(m[1, 1].I))
            );

            long m10_scale = long.Max(ddouble.ILogB(m[1, 0].R), ddouble.ILogB(m[1, 0].I));

            if (diagonal_scale - m10_scale < 106L) {
                Complex b = m[0, 0] + m[1, 1], c = m[0, 0] - m[1, 1];

                Complex d = Complex.Sqrt(c * c + 4 * m[0, 1] * m[1, 0]);

                Complex val0 = (b + d) / 2;
                Complex val1 = (b - d) / 2;

                ComplexVector vec0 = new ComplexVector((c + d) / (2 * m[1, 0]), 1).Normal;
                ComplexVector vec1 = new ComplexVector((c - d) / (2 * m[1, 0]), 1).Normal;

                return (new Complex[] { val0, val1 }, new ComplexVector[] { vec0, vec1 });
            }
            else {
                Complex val0 = m[0, 0];
                Complex val1 = m[1, 1];

                if (val0 != val1) {
                    ComplexVector vec0 = (1, 0);
                    ComplexVector vec1 = new ComplexVector(m[0, 1] / (val1 - val0), 1).Normal;

                    return (new Complex[] { val0, val1 }, new ComplexVector[] { vec0, vec1 });
                }
                else {
                    return (new Complex[] { val0, val1 }, new ComplexVector[] { (1, 0), (0, 1) });
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
    }
}
