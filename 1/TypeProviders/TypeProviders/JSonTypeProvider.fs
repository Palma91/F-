module JsonTypeProvider

open System.IO
open FSharp.Data



type  provider1 = JsonProvider<""" { "first_Name":"aaa", "last_Name":"bbb" } """>   // schema of csv file - not exactly file 


let print ()= 
       
       let data = provider1.Parse(""" {"first_Name": "Иван","last_Name": "Иванов" }  """ )


       printfn "first name: %A  second name: %A " data.FirstName data.LastName


