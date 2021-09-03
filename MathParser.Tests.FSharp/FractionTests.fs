module FractionTests

open System
open Xunit
open EgorLucky.MathParser

let _parser = new MathParser();

[<Fact>]
let ParseFraction() =
    let expression = "1/2/3/4/x"
    let parameter = new Parameter("x", 0.0)

    let variables = new ResizeArray<Variable>([parameter.GetVariable()])

    let result = _parser.TryParse(expression, variables)

    let computedResult = 
        match result.IsSuccessfulCreated with
        | true -> result.Expression.ComputeValue(new ResizeArray<Parameter>([parameter]))
        | _ -> 0.0

    let expectedResult = 1.0 / 2.0 / 3.0 / 4.0 / parameter.Value

    Assert.True(result.IsSuccessfulCreated)
    Assert.Equal("Fraction", result.Expression.Name)
    Assert.Equal(expectedResult, computedResult)