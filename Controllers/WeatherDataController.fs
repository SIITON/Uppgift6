namespace Uppgift6.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.IO
open System.Threading.Tasks
open Microsoft.FSharp.Linq
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Uppgift6

[<ApiController>]
[<Route("[controller]")>]
type WeatherDataController (logger : ILogger<WeatherDataController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member __.Get() : WeatherData[] =
        let csvLines = File.ReadAllLines(@"temperatures.csv")
        let csvData =
            query {
                for lines in csvLines do
                select (lines.Split ';')
            }
        [|
            for data in csvData ->
                { Timestamp = Int32.Parse data.[0]
                  TemperatureC = Double.Parse data.[1]
                  Date = DateTime.Parse data.[2]
                }
        |]
