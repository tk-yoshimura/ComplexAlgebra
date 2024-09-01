using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexVector {
        public static implicit operator ComplexVector((Complex x, Complex y) v) {
            return new ComplexVector([v.x, v.y], cloning: false);
        }

        public void Deconstruct(out Complex x, out Complex y) {
            if (Dim != 2) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (x, y) = (v[0], v[1]);
        }

        public static implicit operator ComplexVector((Complex x, Complex y, Complex z) v) {
            return new ComplexVector([v.x, v.y, v.z], cloning: false);
        }

        public void Deconstruct(out Complex x, out Complex y, out Complex z) {
            if (Dim != 3) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (x, y, z) = (v[0], v[1], v[2]);
        }

        public static implicit operator ComplexVector((Complex x, Complex y, Complex z, Complex w) v) {
            return new ComplexVector([v.x, v.y, v.z, v.w], cloning: false);
        }

        public void Deconstruct(out Complex x, out Complex y, out Complex z, out Complex w) {
            if (Dim != 4) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (x, y, z, w) = (v[0], v[1], v[2], v[3]);
        }

        public static implicit operator ComplexVector((Complex e0, Complex e1, Complex e2, Complex e3, Complex e4) v) {
            return new ComplexVector([v.e0, v.e1, v.e2, v.e3, v.e4], cloning: false);
        }

        public void Deconstruct(out Complex e0, out Complex e1, out Complex e2, out Complex e3, out Complex e4) {
            if (Dim != 5) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4) = (v[0], v[1], v[2], v[3], v[4]);
        }

        public static implicit operator ComplexVector((Complex e0, Complex e1, Complex e2, Complex e3, Complex e4, Complex e5) v) {
            return new ComplexVector([v.e0, v.e1, v.e2, v.e3, v.e4, v.e5], cloning: false);
        }

        public void Deconstruct(out Complex e0, out Complex e1, out Complex e2, out Complex e3, out Complex e4, out Complex e5) {
            if (Dim != 6) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4, e5) = (v[0], v[1], v[2], v[3], v[4], v[5]);
        }

        public static implicit operator ComplexVector((Complex e0, Complex e1, Complex e2, Complex e3, Complex e4, Complex e5, Complex e6) v) {
            return new ComplexVector([v.e0, v.e1, v.e2, v.e3, v.e4, v.e5, v.e6], cloning: false);
        }

        public void Deconstruct(out Complex e0, out Complex e1, out Complex e2, out Complex e3, out Complex e4, out Complex e5, out Complex e6) {
            if (Dim != 7) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4, e5, e6) = (v[0], v[1], v[2], v[3], v[4], v[5], v[6]);
        }

        public static implicit operator ComplexVector((Complex e0, Complex e1, Complex e2, Complex e3, Complex e4, Complex e5, Complex e6, Complex e7) v) {
            return new ComplexVector([v.e0, v.e1, v.e2, v.e3, v.e4, v.e5, v.e6, v.e7], cloning: false);
        }

        public void Deconstruct(out Complex e0, out Complex e1, out Complex e2, out Complex e3, out Complex e4, out Complex e5, out Complex e6, out Complex e7) {
            if (Dim != 8) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4, e5, e6, e7) = (v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7]);
        }
    }
}
