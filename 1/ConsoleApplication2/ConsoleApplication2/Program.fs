// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System;


    //масшабирование 
    let scale (a:float, b:float) (i, j) x =
        float (x-i)/float (j-i)*(b-a)+a;;

    let unscale (i, j) (a:float, b:float)  (f:float) =
        int ((f-a)/(b-a)*float (j-i))+1;;

    let xrange = (0,50);;
    let yrange = (0,15);;

    let plot xdim ydim f = 
        for i = fst yrange to snd yrange do 
            for j = fst xrange to snd xrange do 
                let x = scale xdim xrange j
                let y = unscale yrange ydim (f x)
                Console.Write (if y=i then "*" else "." )
            Console.WriteLine ("")
            
            
            
    plot (-4.0,1.0)(-1.0,1.0) cos
    
        
        (*
[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    
    
    (* построение графика  *)


     0
     
     *)// return an integer exit code
