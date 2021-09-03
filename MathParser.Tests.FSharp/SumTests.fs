module SumTests

open System
open Xunit
open EgorLucky.MathParser

let _parser = new MathParser()

[<Fact>]
let ParseSum () =
    let expression = "2 + 0.5 + 2.5*cos(pi) - log(2, 8) + sin(x) + tg(x)^2"
    let parameter = new Parameter("x", 0.0)
    let variables = new ResizeArray<Variable>([parameter.GetVariable()])
    let result = _parser.TryParse(expression, variables)

    let computedResult = 
        match result.IsSuccessfulCreated with
        | true -> result.Expression.ComputeValue(new ResizeArray<Parameter>([parameter]))
        | _ -> 0.0

    let expectedResult = 2.0 + 
                         0.5 + 
                         2.5 * 
                         Math.Cos(Math.PI) - 
                         Math.Log(8.0, 2.0) + 
                         Math.Sin(parameter.Value) + 
                         Math.Pow(Math.Tan(parameter.Value), 2.0)
    Assert.True(result.IsSuccessfulCreated)
    Assert.Equal("Sum", result.Expression.Name)
    Assert.Equal(expectedResult, computedResult)

[<Fact>]
let ParseSumWithManyPlusesInTheEnd() =
    let expression = "2 + 0.5 + 2.5*cos(pi) - log(2, 8) + sin(x) + tg(x)^2++++++"
    let parameter = new Parameter("x", 0.0)

    let variables = new ResizeArray<Variable>([parameter.GetVariable()])

    let result = _parser.TryParse(expression, variables)

    Assert.False(result.IsSuccessfulCreated)

[<Fact>]
let ParseSumWithMany_F_LettersInTheEnd() =
    let expression = "2 + 0.5 + 2.5*cos(pi) - log(2, 8) + sin(x) + tg(x)^2ffffffff"
    let parameter = new Parameter("x", 0.0)

    let variables = new ResizeArray<Variable>([parameter.GetVariable()])

    let result = _parser.TryParse(expression, variables)

    Assert.False(result.IsSuccessfulCreated)

[<Fact>]
let ParseSumWithUnexistExpression() =
    let expression = "2 + 0.5 + 2.5*cos(pi) - log(2, 8) + sin(x) + unexistng(x)^2"
    let parameter = new Parameter("x", 0.0)

    let variables = new ResizeArray<Variable>([parameter.GetVariable()])

    let result = _parser.TryParse(expression, variables)

    Assert.False(result.IsSuccessfulCreated)