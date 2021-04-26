module PowTests

open System
open Xunit
open EgorLucky.MathParser

let _parser = new MathParser();

[<Fact>]
let ParsTgSquared() = 
    let expression = "tg(x)^2";
    let parameter = new Parameter(VariableName = "x",
                                  Value = float 0)

    let variables = new ResizeArray<Variable>([parameter.GetVariable()])

    let result = _parser.TryParse(expression, variables)

    let mutable computedResult = float 0
    if result.IsSuccessfulCreated
    then computedResult <- result.Expression.ComputeValue(new ResizeArray<Parameter>([parameter]))
    let expectedResult = Math.Pow(Math.Tan(parameter.Value), float 2)

    Assert.True(result.IsSuccessfulCreated)
    Assert.Equal("Pow", result.Expression.Name)
    Assert.Equal(expectedResult, computedResult)