USE University

CREATE TABLE dbo.Students (
	Id INT IDENTITY NOT NULL PRIMARY KEY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50),
	DateOfBirth DATE NOT NULL
	)

BULK INSERT dbo.Students
FROM 'C:\git\student-test\SQL\Students.csv'
WITH (
	FIRSTROW = 2,
	FORMAT = 'CSV'
	)