USE university
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE uspInsertStudent
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@DateOfBirth DATE,
	@YearOfStudy VARCHAR(10)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO dbo.Students (FirstName, LastName, DateOfBirth, YearOfStudy)
	OUTPUT Inserted.ID
	VALUES (@FirstName, @LastName, @DateOfBirth, @YearOfStudy)
END
GO