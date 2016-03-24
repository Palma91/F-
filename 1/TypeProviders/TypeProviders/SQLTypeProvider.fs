module SQLTypeProvider

open System.IO
open FSharp.Data.Sql

//http://fsharp.github.io/FSharp.Data/library/JsonProvider.html


    type sql = SqlDataProvider<"Server=.;Initial Catalog = ">





let print ()= 

    let context = sql.GetDataContext()

    context.'sdas'
        |> Seq.iter (fun p -> printfn "sadasd %A" p.LastName)

