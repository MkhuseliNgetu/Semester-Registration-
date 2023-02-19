CREATE PROCEDURE SP_CreateANewStudent(
@StudentName VARCHAR(150) = '',
@StudentPassword INT = 0
)
AS
BEGIN
INSERT INTO Student
VALUES(@StudentName,@StudentPassword);
END

