module JSonTypeProviderFromFile

open System.IO
open FSharp.Data

//http://fsharp.github.io/FSharp.Data/library/JsonProvider.html



type WorldBank = JsonProvider<"Countries.json">
let doc = WorldBank.GetSample()





let print ()= 

//H:/Repositories/F-/1/TypeProviders/TypeProviders/Countries.json
    //   let data = provider.Parse("H:/Repositories/F-/1/TypeProviders/TypeProviders/Countries.json" )
   //    let data = provider.Parse("Countries.json" )

       
    
    //   let docAsync = provider.AsyncLoad("http://api.worldbank.org/countries")


         // Print general information
        //let info = doc.Record
        
// Print general information
        let info = doc.Record
        printfn "Showing page %d of %d. Total records %d" 
          info.Page info.Pages info.Total

        // Print all data points
        for record in doc.Array do
          record.Value |> Option.iter (fun value ->
            printfn "%d: %f" record.Date value)


        // Print all data points
//        for record in doc.Array do
//          record.Value |> Option.iter (fun value ->
//         printfn "%d: %f" record.Date value)
//


  //     printfn "first name: %A " docAsync.Id


