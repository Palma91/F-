module HTTPProvider

open System.IO
open FSharp.Data
//open FSharp.Net

//http://fsharp.github.io/FSharp.Data/library/JsonProvider.html

//let key = ""

//let url = "http..." + key

let data = Http.Request(url,headers=["accept", "application/json"])  //json data are back 
// copy to file 


type WorldBank = JsonProvider<"file.json">



let searchResults = WorldBank.Parse(data)
searchResults. /// should be defined





let print ()= 