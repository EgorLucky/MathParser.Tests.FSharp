module ExtensionsTests

open System
open Xunit
open EgorLucky.MathParser

let _parser = new MathParser()

[<Fact>]
let ExtensionTest () =
    let expression = "x+y+2*z"
    let x = 0.0
    let y = 20.0
    let z = 1.1

    let result = _parser.TryParse(expression, "x", "y", "z")

    let computedResult =
        match result.IsSuccessfulCreated with
        | true -> result.Expression.ComputeValue(x, y, z)
        | _ -> 0.0
        
    let expectedResult = x + y + 2.0 * z;

    Assert.True(result.IsSuccessfulCreated);
    Assert.Equal("Sum", result.Expression.Name);
    Assert.Equal(expectedResult, computedResult);
    