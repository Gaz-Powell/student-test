USE University

CREATE TABLE dbo.Enrolments (
	Id INT IDENTITY NOT NULL PRIMARY KEY,
	StudentId INT FOREIGN KEY REFERENCES dbo.Students(Id),
	SubjectId INT FOREIGN KEY REFERENCES dbo.Subjects(Id)
	)

INSERT INTO dbo.Enrolments (StudentId, SubjectId)
VALUES
	(1, 1),
	(1, 4),
	(2, 1),
	(2, 4),
	(3, 1),
	(3, 4),
	(4, 1),
	(4, 4),
	(5, 1),
	(5, 4),
	(6, 2),
	(7, 2),
	(8, 2),
	(9, 2),
	(10, 2),
	(11, 4),
	(12, 4),
	(13, 4),
	(14, 4),
	(15, 4),
	(16, 5),
	(16, 3),
	(17, 5),
	(17, 3),
	(18, 5),
	(18, 3),
	(19, 5),
	(19, 3),
	(20, 5),
	(20, 3)