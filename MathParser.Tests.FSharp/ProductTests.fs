module ProductTests

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
let ParseProduct() =
    let expression = "2 * 0.5 * -2.5*cos(pi)* tg(x)^2 * log(-2, -8) * sin(x)*(2+3)*(cos(0)+sin(0))*(2*(x+1))"
    let parameter = new Parameter("x", 1.0)

    let variables = new ResizeArray<Variable>([parameter.GetVariable()])

    let result = _parser.TryParse(expression, variables)

    let mutable computedResult = float 0
    if result.IsSuccessfulCreated
    then computedResult <- result.Expression.ComputeValue(new ResizeArray<Parameter>([ parameter ]))

    let expectedResult = float 2 * 0.5 * -2.5 
                            * Math.Cos(Math.PI) 
                            * Math.Pow(Math.Tan(parameter.Value), float 2) 
                            * Math.Log(float -8, float -2) 
                            * Math.Sin(parameter.Value)
                            * float (2+3)
                            * (Math.Cos(float 0) + Math.Sin(float 0))
                            * (float 2 * (parameter.Value + float 1))

    
    Assert.True(result.IsSuccessfulCreated)
    Assert.Equal("Product", result.Expression.Name)
    Assert.Equal(expectedResult, computedResult)

[<Fact>]
let ParseProductWithUnexistFunction() =
    let expression = "2 * 0.5 * 2.5*kek(pi)* tg(x)^2 * log(2, 8) * sin(x) "
    let parameter = new Parameter("x", 0.0)

    let variables = new ResizeArray<Variable>([parameter.GetVariable()])

    let result = _parser.TryParse(expression, variables)

    Assert.False(result.IsSuccessfulCreated)