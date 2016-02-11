#r "packages/FSharp.Data.2.2.5/lib/net40/FSharp.Data.dll"

open FSharp.Data


let wb = WorldBankData.GetDataContext()
wb.Countries.Afghanistan.CapitalCity
wb.Countries.Afghanistan.Indicators.``Death rate, crude (per 1,000 people)``.[2012]


for countries in wb.Regions.``Euro area``.Countries do 
printfn  "%s, %s" countries.Name countries.CapitalCity