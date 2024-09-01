using DoubleDoubleComplex;
using System.Diagnostics;

namespace ComplexAlgebra {
    public partial class ComplexMatrix {
        private static Complex Det2x2(ComplexMatrix m) {
            Debug.Assert(m.Shape == (2, 2));

            Complex det = m.e[0, 0] * m.e[1, 1] - m.e[0, 1] * m.e[1, 0];

            return det;
        }

        private static Complex Det3x3(ComplexMatrix m) {
            Debug.Assert(m.Shape == (3, 3));

            Complex det =
                m.e[0, 0] * (m.e[1, 1] * m.e[2, 2] - m.e[2, 1] * m.e[1, 2]) +
                m.e[1, 0] * (m.e[2, 1] * m.e[0, 2] - m.e[0, 1] * m.e[2, 2]) +
                m.e[2, 0] * (m.e[0, 1] * m.e[1, 2] - m.e[1, 1] * m.e[0, 2]);

            return det;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex Det {
            get {
                if (Shape == (1, 1)) {
                    return e[0, 0];
                }
                if (Shape == (2, 2)) {
                    return Det2x2(this);
                }
                if (Shape == (3, 3)) {
                    return Det3x3(this);
                }

                (_, int pivot_det, _, ComplexMatrix u) = LUKernel(this);

                Complex prod = pivot_det;
                foreach (var diagonal in u.Diagonals) {
                    prod *= diagonal;
                }

                return prod;
            }
        }
    }
}
