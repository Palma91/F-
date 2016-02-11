




[<Literal>]
let apiUrl  = "http://demo-cd.systemorph.local/api/Policies"

type NAVService = ODataService<apiUrl>

db.DataContext.SendingRequest.Add (fun eventArgs -> printfn "Requesting %A" eventArgs.Request.RequestUri)


let nav = NAVService.GetDataContext()
nav.Credentials <- System.Net.CredentialCache.DefaultCredentials
   