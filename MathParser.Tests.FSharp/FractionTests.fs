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

    let mutable computedResult = float 0
    if result.IsSuccessfulCreated
    then computedResult <- result.Expression.ComputeValue(new ResizeArray<Parameter>([parameter]))
    let expectedResult = float 1 / float 2 / float 3 / float 4 / parameter.Value

    Assert.True(result.IsSuccessfulCreated)
    Assert.Equal("Fraction", result.Expression.Name)
    Assert.Equal(expectedResult, computedResult)