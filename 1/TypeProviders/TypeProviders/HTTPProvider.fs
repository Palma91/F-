module HTTPProvider


open FSharp.Data.TypeProviders
open System.IO
open FSharp.Data
open System.Linq
//open FSharp.Net

//http://fsharp.github.io/FSharp.Data/library/JsonProvider.html

//let key = ""

let url = "http://api.worldbank.org/country/cz/indicator/GC.DOD.TOTL.GD.ZS" //+ key
let url2 = "http://api.worldbank.org/country/cz/indicator/GC.DOD.TOTL.GD.ZS?format=json"
let data = Http.Request(url,headers=["accept", "application/json"])  //json data are back 
// copy to file 
let data2 = Http.Request(url2,headers=["accept", "application/json"])  //json data are back 


type WorldBank = JsonProvider<"WorldBank.json">
let samples = WorldBank.Load("http://api.worldbank.org/country/cz/?format=json")


query{
for sample in samples.Array do
   select sample }
   |> Seq.iter (fun sample -> 
   printfn "%s" sample.CapitalCity
   printfn "\n")



query{
for sample in samples.Array do
   select sample }
   |> Seq.iter (fun sample -> 
   printfn "%s" sample.Name
   printfn "\n")



