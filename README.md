# University Students

## Solution

The solution is organised into separate projects responsible for the different layers of the system. One exception is including the StudentsService in the Web project; I would normally create a separate Application project to hold the business logic (or logic would be grouped with the models in a Domain-Driver Design solution).

I have used Dapper and stored procedures to access the database, as that is what I am currently most familiar with. I investigated using Entity Framework and will continue to implement this for my own progression.

## Future Improvements

The frontend has only basic Bootstrap styling and minimal UI. With more time I would update the user experience to fix formatting and include validation via MVC, with user notifications to confirm actions being requested by the user and completed by the system.