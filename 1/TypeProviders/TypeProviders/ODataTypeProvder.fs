module ODataTypeProvder
open Microsoft.FSharp.Linq.NullableOperators
open System.IO
open FSharp.Data
open FSharp.Data.TypeProviders


type Northwind = ODataService<"http://services.odata.org/Northwind/Northwind.svc/">

let db = Northwind.GetDataContext()
let fullContext = Northwind.ServiceTypes.NorthwindEntities()

query { for customer in db.Customers do
        select customer }
        |> Seq.iter (fun customer ->
            printfn "ID: %s\nCompany: %s" customer.CustomerID customer.CompanyName
            printfn "Contact: %s\nAddress: %s" customer.ContactName customer.Address
            printfn "         %s, %s %s" customer.City customer.Region customer.PostalCode
            printfn "%s\n" customer.Phone)


query { for cat in db.Categories do
        select (cat.CategoryID, cat.CategoryName, cat.Description) }
        |> Seq.iter (fun (id, name, description) ->
        printfn "ID: %d\nCategory: %s\nDescription: %s\n" id name description)


query { for employee in db.Employees do
        where (employee.EmployeeID = 9)
        select employee }
        |> Seq.iter (fun employee ->
        printfn "Name: %s ID: %d" (employee.FirstName + " " + employee.LastName) (employee.EmployeeID))                         


query { for product in db.Products do
        where (product.ProductName.Contains("Chef"))
        select product }
        |> Seq.iter (fun product ->
            printfn "ID: %d Product: %s" product.ProductID product.ProductName
            printfn "Price: %M\n" (product.UnitPrice.GetValueOrDefault()))


query { for product in db.Products do
        where (product.ProductName.EndsWith("u"))
            select product }
            |> Seq.iter (fun product ->
            printfn "ID: %d Product: %s" product.ProductID product.ProductName
            printfn "Price: %M\n" (product.UnitPrice.GetValueOrDefault()))


let salesIn1997 = query { for sales in db.Category_Sales_for_1997 do
                          where (sales.CategorySales ?> 50000.00M && sales.CategorySales ?< 60000.0M)
                          select sales }
                salesIn1997
                |> Seq.iter (fun sales ->
                    printfn "Category: %s Sales: %M" sales.CategoryName (sales.CategorySales.GetValueOrDefault()))


printfn "Freight for some orders: "
query { for order in db.Orders do
        sortBy (order.OrderDate.Value)
        thenBy (order.OrderID)
        select (order.OrderDate, order.OrderID, order.Customer.CompanyName)
         }
|> Seq.iter (fun (orderDate, orderID, company) ->
    printfn "OrderDate: %s" (orderDate.GetValueOrDefault().ToString())
    printfn "OrderID: %d Company: %s\n" orderID company)



    printfn "Get the first page of 2 employees."
    query { for employee in db.Employees do
            take 2
            select employee }
    |> Seq.iter (fun employee ->
    printfn "Name: %s ID: %d" (employee.FirstName + " " + employee.LastName) (employee.EmployeeID)) 

    printfn "Get the next 2 employees."
    query { for employee in db.Employees do
            skip 2
            take 2
            select employee }
    |> Seq.iter (fun employee ->
    printfn "Name: %s ID: %d" (employee.FirstName + " " + employee.LastName) (employee.EmployeeID)) 





     db.DataContext.SendingRequest.Add (fun eventArgs -> printfn "Requesting %A" eventArgs.Request.RequestUri)
