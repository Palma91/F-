

open System
open System.Data
open System.Data.Linq
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Data.TypeProviders

[<EntryPoint>]
let main argv = 
{
    printfn "%A" argv
    0 // return an integer exit code

    type dbSchema = SqlDataConnection<"Data Source=(localdb)\ProjectsV12;Initial Catalog=Database1;Integrated Security=True;Connect Timeout=30;">
    let db = dbSchema.GetDataContext()


    db.DataContext.Log <- System.Console.Out
    
    let query = 
            query {
                    for row in db.AnotherStudentTable do 
                    select row
            }
            
    query |> Seq.iter (fun row -> printfn "qqwewqe %d" row.StudentID)
0
}

     // return an integer exit code
