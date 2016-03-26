module JsonTypeProvider

open System.IO
open FSharp.Data
open FSharp.Data.TypeProviders


type  provider1 = JsonProvider<""" { "first_Name":"aaa", "last_Name":"bbb" } """>   // schema of csv file - not exactly file 


let print ()= 
       
       let data = provider1.Parse(""" {"first_Name": "Иван","last_Name": "Иванов" }  """ )


       printfn "first name: %A  second name: %A " data.FirstName data.LastName


       

	

	

type WorldBank = JsonProvider<"WorldBank.json">
let doc = WorldBank.GetSample()


let docAsync = WorldBank.AsyncLoad("http://api.worldbank.org/country/cz/indicator/GC.DOD.TOTL.GD.ZS?format=json")
use docAsync = 
        match docAsync with
        | Some (sw) -> sw
        | None -> new MemoryStream()


let searchResults = WorldBank.Parse(docAsync)
docAsync.
