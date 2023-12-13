USE university
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE uspUpdateStudent
	@Id INT,
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@DateOfBirth DATE

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE dbo.Students 
	SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth
	WHERE Id = @Id
END
GO