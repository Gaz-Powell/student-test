### Question 1
Please list any improvements you think can be made to the above code.

- Change the method name to “Details” instead of “details” – the usual C# naming conventions dictate method names should be in PascalCase.
- Move the database connection string into appsettings.json.
- Move the database-specific code into a separate class to handle creating the connection (receiving the connection string above) and ensuring the connection is correctly disposed, so resources are released once the request is completed. This connection would then be provided via dependency injection to the controller (or to a repository or service in between the controller and the persistence layer).
- Use an ORM such as Entity Framework or Dapper to construct a query using LINQ, or executed a stored procedure.
- Use the int (or uint) type for ID instead of string – the current code is at high risk of a SQL injection attack.
- Create a strongly typed view model to return the data, rather than using the weakly typed ViewBag – this provides IntelliSense assistance to reduce coding errors and removes the need for type casting.


### Question 2

- GET https://made-up-api.com/v1/customers
- GET https://made-up-api.com/v1/orders/25
- DELETE https://made-up-api.com/v1/customers/133
- PATCH https://made-up-api.com/v1/orders
- PUT https://made-up-api.com/v1/customers/1701
