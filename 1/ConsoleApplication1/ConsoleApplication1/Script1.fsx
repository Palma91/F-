#r "System.Data.dll"
#r "System.Data.Linq.dll"
#r "FSharp.Data.TypeProviders.dll"



open System
open System.Data
open System.Data.Linq
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Data.TypeProviders

    type dbSchema = SqlDataConnection<"Data Source=(localdb)\ProjectsV12;Initial Catalog=Database1;Integrated Security=True;Connect Timeout=30;">
    let db = dbSchema.GetDataContext()


    db.DataContext.Log <- System.Console.Out
    
    let query1 = 
            query {
               for row in db.Course do 
               select row
                    }
            
    query1 |> Seq.iter (fun row -> printfn "qqwewqe %d" row.CourseID)




    let allTables = 
        query {
            for table in sys.databases where name = "MyDatabase" do
             select table
        }


        
    allTables |> Seq.iter (fun table -> printfn "qqwewqe %d" table.name)
