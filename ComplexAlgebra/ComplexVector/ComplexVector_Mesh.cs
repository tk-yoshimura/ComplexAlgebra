using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexVector {
        public static (ComplexVector x, ComplexVector y) MeshGrid(ComplexVector x, ComplexVector y) {
            int n = checked(x.Dim * y.Dim);

            Complex[] rx = x.v, ry = y.v;
            Complex[] vx = new Complex[n], vy = new Complex[n];

            for (int i = 0, idx = 0; i < y.Dim; i++) {
                for (int j = 0; j < x.Dim; j++, idx++) {
                    vx[idx] = rx[j];
                    vy[idx] = ry[i];
                }
            }

            return (
                new ComplexVector(vx, cloning: false),
                new ComplexVector(vy, cloning: false)
            );
        }

        public static (ComplexVector x, ComplexVector y, ComplexVector z) MeshGrid(ComplexVector x, ComplexVector y, ComplexVector z) {
            int n = checked(x.Dim * y.Dim * z.Dim);

            Complex[] rx = x.v, ry = y.v, rz = z.v;
            Complex[] vx = new Complex[n], vy = new Complex[n], vz = new Complex[n];

            for (int i = 0, idx = 0; i < z.Dim; i++) {
                for (int j = 0; j < y.Dim; j++) {
                    for (int k = 0; k < x.Dim; k++, idx++) {
                        vx[idx] = rx[k];
                        vy[idx] = ry[j];
                        vz[idx] = rz[i];
                    }
                }
            }

            return (
                new ComplexVector(vx, cloning: false),
                new ComplexVector(vy, cloning: false),
                new ComplexVector(vz, cloning: false)
            );
        }

        public static (ComplexVector x, ComplexVector y, ComplexVector z, ComplexVector w) MeshGrid(ComplexVector x, ComplexVector y, ComplexVector z, ComplexVector w) {
            int n = checked(x.Dim * y.Dim * z.Dim * w.Dim);

            Complex[] rx = x.v, ry = y.v, rz = z.v, rw = w.v;
            Complex[] vx = new Complex[n], vy = new Complex[n], vz = new Complex[n], vw = new Complex[n];

            for (int i = 0, idx = 0; i < w.Dim; i++) {
                for (int j = 0; j < z.Dim; j++) {
                    for (int k = 0; k < y.Dim; k++) {
                        for (int m = 0; m < x.Dim; m++, idx++) {
                            vx[idx] = rx[m];
                            vy[idx] = ry[k];
                            vz[idx] = rz[j];
                            vw[idx] = rw[i];
                        }
                    }
                }
            }

            return (
                new ComplexVector(vx, cloning: false),
                new ComplexVector(vy, cloning: false),
                new ComplexVector(vz, cloning: false),
                new ComplexVector(vw, cloning: false)
            );
        }
    }
}
