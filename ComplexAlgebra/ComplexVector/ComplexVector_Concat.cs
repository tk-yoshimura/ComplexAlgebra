﻿using Algebra;
using DoubleDouble;
using DoubleDoubleComplex;

namespace ComplexAlgebra {
    public partial class ComplexVector {
        public static ComplexVector Concat(params object[] blocks) {
            List<Complex> v = [];

            foreach (object obj in blocks) {
                if (obj is ComplexVector cvector) {
                    v.AddRange(cvector.v);
                }
                else if (obj is Vector vector) {
                    v.AddRange(vector.Select(item => (Complex)item.val));
                }
                else if (obj is Complex vmpc) {
                    v.Add(vmpc);
                }
                else if (obj is ddouble vmp) {
                    v.Add(vmp);
                }
                else if (obj is System.Numerics.Complex vdc) {
                    v.Add(vdc);
                }
                else if (obj is double vd) {
                    v.Add(vd);
                }
                else if (obj is int vi) {
                    v.Add(vi);
                }
                else if (obj is long vl) {
                    v.Add(vl);
                }
                else if (obj is float vf) {
                    v.Add(vf);
                }
                else if (obj is string vs) {
                    v.Add(vs);
                }
                else {
                    throw new ArgumentException($"unsupported type '{obj.GetType().Name}'", nameof(blocks));
                }
            }

            return new ComplexVector(v);
        }

        public static ComplexVector Concat(params ComplexVector[] blocks) {
            List<Complex> v = [];

            foreach (ComplexVector vector in blocks) {
                v.AddRange(vector.v);
            }

            return new ComplexVector(v);
        }
    }
}
