#r "packages/FSharp.Data.2.2.5/lib/net40/FSharp.Data.dll"
#r "FSharp.Data.TypeProviders.dll"


open System
open System.Security
open FSharp.Data
open Microsoft.FSharp.Data.TypeProviders



[<Literal>]
//let apiUrl  = "http://services.odata.org/Northwind/Northwind.svc/"
//let apiUrl  = "https://api.datamarket.azure.com/Esri/KeyUSDemographicsTrial/"

//type Northwind = ODataService<apiUrl>

//let db = Northwind.GetDataContext()


    type Demographics = ODataService<ServiceUri = "https://api.datamarket.azure.com/Esri/KeyUSDemographicsTrial/">
    let ctx = Demographics.GetDataContext()



   // db.Credentials <- System.Net.NetworkCredential ("alena.makrushina@sidenis.com", "***")

    
let cities = query {
    for c in db.Categories do
    where (c.AsBoolean = "Washington")
    } 



 for c in cities do
        printfn "%A - %A" c.GeographyId c.PerCapitaIncome2010.Value







let fullContext = Northwind.ServiceTypes.Product.CreateProduct(1,"qwe",false)

        for supl in db do 
            yield supl



             for word in seq2 do
                  if word.Contains("l") then 
                      yield word 

query { for db1 in db.Categories do
        }
|> Seq.iter (fun customer ->  printfn "ID: %s\nEmployees:" db1
    
    CustomerID customer.CompanyName
    printfn "Contact: %s\nAddress: %s" customer.ContactName customer.Address
    printfn "         %s, %s %s" customer.City customer.Region customer.PostalCode
    printfn "%s\n" customer.Phone)
