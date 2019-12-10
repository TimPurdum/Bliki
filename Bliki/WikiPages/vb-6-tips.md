<!-- TITLE: Vb 6 Tips -->
<!-- SUBTITLE: A quick summary of Vb 6 Tips -->

# General VB6 Tips + Tricks

## Marshalling values between VB6 and C#

### Object Arrays
If you want to pass an object[] from C# to VB6:
```vbscript
Option Explicit

Dim objArray() As Variant

objArray = CSharpComVisibleClass.MethodThatReturnsObjectArray

Dim i As Integer
For i = LBound(objArray) To UBound(objArray)
   ' i will always be a valid index into objArray
	 ' Do remember that objArray(i) will be a Variant
Next
```

In C#, you must declare the array as an `object[]` and not an array of the specific type.
```csharp
// Correct declaration:
var array = new object[SIZE];

// Will not work:
var array = new Dto[SIZE];
```
(I think) this is because of C#'s covariance that lets you automatically assign a `dto[]` to an `object[]`. While that works fine for C#, VB6 is expecting actual `object`s.