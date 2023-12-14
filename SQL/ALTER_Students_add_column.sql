USE University

ALTER TABLE dbo.Students
	ADD YearOfStudy VARCHAR(10) 
	CHECK (YearOfStudy IN ('1st Year', '2nd Year', '3rd Year'))
