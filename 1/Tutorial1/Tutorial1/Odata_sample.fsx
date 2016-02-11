#r "packages/FSharp.Data.2.2.5/lib/net40/FSharp.Data.dll"
#r "FSharp.Data.TypeProviders.dll"


open System
open System.Security
open FSharp.Data
open Microsoft.FSharp.Data.TypeProviders



[<Literal>]
let apiUrl  = "http://services.odata.org/Northwind/Northwind.svc/"

type Northwind = ODataService<apiUrl>

let db = Northwind.GetDataContext()
let fullContext = Northwind.ServiceTypes.NorthwindEntities()
