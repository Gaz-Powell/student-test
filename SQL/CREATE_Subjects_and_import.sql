USE University

CREATE TABLE dbo.Subjects (
	Id INT IDENTITY NOT NULL PRIMARY KEY,
	Subject NVARCHAR(255) NOT NULL
	)

BULK INSERT dbo.Subjects
FROM 'C:\git\student-test\SQL\Subjects.csv'
WITH (
	FIRSTROW = 2,
	FORMAT = 'CSV'
	)