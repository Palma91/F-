// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

#r "H:/Repositories/F-/1/Tutorial1/packages/FSharp.Data.TypeProviders.4.3.0.0/Type Providers/FSharp.Data.TypeProviders.dll"
#r "H:/Repositories/F-/1/Tutorial1/Tutorial1/packages/FSharp.Data.2.2.5/lib/net40/FSharp.Data.dll"

open FSharp.Data


let results = HtmlDocument.Load("http://www.google.co.uk/search?q=FSharp.Data")

let links = 
    results.Descendants ["a"]
    |> Seq.choose (fun x -> 
           x.TryGetAttribute("href")
           |> Option.map (fun a -> x.InnerText(), a.Value())
    )
    
let searchResults =
    links
    |> Seq.filter (fun (name, url) -> 
                    name <> "Cached" && name <> "Similar" && url.StartsWith("/url?"))
    |> Seq.map (fun (name, url) -> name, url.Substring(0, url.IndexOf("&sa=")).Replace("/url?q=", ""))
    |> Seq.toArray




let doctorWho = new HtmlProvider<"http://en.wikipedia.org/wiki/List_of_Doctor_Who_serials">()

// Get the average number of viewers for each doctor
let viewersByDoctor = 
    doctorWho.Tables.Overview.Rows 
    |> Seq.groupBy (fun season -> season.``Doctor(s)``)
    |> Seq.map (fun (doctor, seasons) -> doctor, seasons |> Seq.averageBy (fun season -> season.``Viewers (millions) - Average``))
    |> Seq.toArray

// Visualize it
(Chart.Column viewersByDoctor).WithYAxis(Title = "Millions")
