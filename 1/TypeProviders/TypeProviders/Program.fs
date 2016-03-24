// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

module TypeProvidersExample

open System


[<EntryPoint>]
let main argv = 

    //CSVTypeProvider.print() |> ignore
    //JsonTypeProvider.print() |> ignore
    
    JSonTypeProviderFromFile.print() |> ignore

    printfn "Press any key..." 
    System.Console.ReadKey() |> ignore


    0 // return an integer exit code
