module PowTests

open System
open Xunit
open EgorLucky.MathParser

let _parser = new MathParser();

[<Fact>]
let ParsTgSquared() = 
    let expression = "tg(x)^2";
    let parameter = new Parameter("x", 0.0)

    let variables = new ResizeArray<Variable>([parameter.GetVariable()])

    let result = _parser.TryParse(expression, variables)


    let computedResult = 
        match result.IsSuccessfulCreated with
        | true -> result.Expression.ComputeValue(new ResizeArray<Parameter>([parameter]))
        | _ -> 0.0

    let expectedResult = Math.Pow(Math.Tan(parameter.Value), 2.0)

    Assert.True(result.IsSuccessfulCreated)
    Assert.Equal("Pow", result.Expression.Name)
    Assert.Equal(expectedResult, computedResult)