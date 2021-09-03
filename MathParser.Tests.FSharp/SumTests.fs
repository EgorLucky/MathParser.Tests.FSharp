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

    let mutable computedResult = 0.0
    if result.IsSuccessfulCreated
    then computedResult <- result.Expression.ComputeValue(new ResizeArray<Parameter>([parameter]))
    let expectedResult = float 2 + 
                         0.5 + 
                         2.5 * 
                         Math.Cos(Math.PI) - 
                         Math.Log(float 8, float 2) + 
                         Math.Sin(parameter.Value) + 
                         Math.Pow(Math.Tan(parameter.Value), float 2)
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