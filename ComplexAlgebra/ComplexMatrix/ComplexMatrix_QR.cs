﻿using DoubleDouble;
using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexMatrix {
        public static (ComplexMatrix q, ComplexMatrix r) QR(ComplexMatrix m) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;

            if (!IsFinite(m)) {
                return (Invalid(n), Invalid(n));
            }
            if (IsZero(m)) {
                return (Zero(n), Zero(n));
            }

            int exponent = m.MaxExponent;
            m = ScaleB(m, -exponent);

            ComplexMatrix r = m, q = Identity(n);
            ComplexVector u = ComplexVector.Zero(n);

            for (int k = 0; k < n - 1; k++) {
                ddouble vsum = ddouble.Zero;
                for (int i = k; i < n; i++) {
                    vsum += r.e[i, k].SquareNorm;
                }
                ddouble vnorm = ddouble.Sqrt(vsum);

                if (ddouble.IsZero(vnorm)) {
                    continue;
                }

                Complex x = r.e[k, k];
                u.v[k] = Complex.IsZero(x) ? vnorm : (x + x / x.Norm * vnorm);
                ddouble usum = u.v[k].SquareNorm;

                for (int i = k + 1; i < n; i++) {
                    u.v[i] = r.e[i, k];
                    usum += u.v[i].SquareNorm;
                }
                ddouble c = 2 / usum;

                ComplexMatrix h = Identity(n);
                for (int i = k; i < n; i++) {
                    for (int j = k; j < n; j++) {
                        h.e[i, j] -= c * u[i] * u[j].Conj;
                    }
                }

                r = h * r;
                q *= h;
            }

            for (int i = 0; i < n; i++) {
                if (ddouble.IsNegative(r.e[i, i].R)) {
                    q[.., i] = -q[.., i];
                    r[i, ..] = -r[i, ..];
                }

                for (int k = 0; k < i; k++) {
                    r.e[i, k] = Complex.Zero;
                }

                for (int k = i + 1; k < n; k++) {
                    if (Complex.IsZero(r.e[i, k])) {
                        r.e[i, k] = Complex.Zero;
                    }
                }
            }

            r = ScaleB(r, exponent);

            return (q, r);
        }
    }
}
