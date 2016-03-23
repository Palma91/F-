module JSonTypeProviderFromFile

open System.IO
open FSharp.Data

//http://fsharp.github.io/FSharp.Data/library/JsonProvider.html

type  provider = JsonProvider<"Countries.json", EmbeddedResource="MyLib, Countries.json">

let doc = provider.GetSample()


let print ()= 
       

       let docAsync = provider.AsyncLoad("http://api.worldbank.org/countries")


         // Print general information
        let info = doc.Record
        printfn "Showing page %d of %d. Total records %d" 
          info.Page info.Pages info.Total

        // Print all data points
        for record in doc.Array do
          record.Value |> Option.iter (fun value ->
         printfn "%d: %f" record.Date value)



       printfn "first name: %A " docAsync.Id


