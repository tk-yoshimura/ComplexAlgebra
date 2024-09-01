using DoubleDoubleComplex;
using System.Diagnostics;

namespace ComplexAlgebra {
    public partial class ComplexMatrix {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex Trace {
            get {
                Complex sum = Complex.Zero;
                foreach (var diagonal in Diagonals) {
                    sum += diagonal;
                }

                return sum;
            }
        }
    }
}
