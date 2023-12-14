USE University
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE uspGetEnrolmentsByStudentId 
	@StudentId INT 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT s.[Subject] FROM dbo.Enrolments e
		LEFT JOIN dbo.Subjects s ON e.SubjectId = s.Id
		WHERE StudentId = @StudentId
END
GO
