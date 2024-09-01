# ComplexAlgebra
 Complex Algebra Implements

## Requirement
.NET 8.0  
AVX2 suppoted CPU. (Intel:Haswell(2013)-, AMD:Excavator(2015)-)  
[DoubleDouble](https://github.com/tk-yoshimura/DoubleDouble)  
[DoubleDoubleComplex](https://github.com/tk-yoshimura/DoubleDoubleComplex)  
[Algebra](https://github.com/tk-yoshimura/Algebra)  

## Install

[Download DLL](https://github.com/tk-yoshimura/ComplexAlgebra/releases)  
[Download Nuget](https://www.nuget.org/packages/tyoshimura.complexalgebra/)

## Usage

```csharp
// solve for v: Av=x
ComplexMatrix a = new Complex[,] { { (1, 1), (1, 2) }, { (1, 3), (4, -1) } };
ComplexVector x = ((4, 2), (-1, 3));

ComplexVector v = ComplexMatrix.Solve(a, x);
```

## Licence
[MIT](https://github.com/tk-yoshimura/ComplexAlgebra/blob/master/LICENSE)

## Author

[T.Yoshimura](https://github.com/tk-yoshimura)
