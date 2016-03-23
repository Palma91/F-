module CSVTypeProvider


open System.IO
open FSharp.Data

type  provider = CsvProvider<"H:\Repositories\F-\1\TypeProviders\TypeProviders\Sample3.csv">   // schema of csv file - not exactly file 

let print () = 

    let data = File.OpenRead("H:\Repositories\F-\1\TypeProviders\TypeProviders\Sample4.csv")

    let csv = provider.Load data 


    let firstRow = csv.Rows |> Seq.head
    let lastDate = firstRow.SN
    let lastOpen = firstRow.DN




    let entities = csv.Rows 
                    |> Seq.map (fun row -> row )
                    //|> Seq.filter (fun row -> row.Type.Equals "Geo" )
                    |> Seq.filter (fun row -> row.Distance <> 1000 )
                    |> Seq.toArray


    entities
        |> Seq.iter (fun entities -> printfn "%A %A " entities.Type entities.DN)