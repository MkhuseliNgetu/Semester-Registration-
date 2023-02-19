CREATE PROCEDURE SP_StudentLogin(
@StudentName VARCHAR(150) = '',
@StudentPassword INT=0
)
AS
BEGIN
SELECT ModuleCode,ModuleName,ModuleCredits,ModuleClassHours,ModuleSelfStudyHours,RemainingSelfStudyHours
FROM PersonalUserModules,Student
WHERE @StudentName=StudentName AND @StudentPassword=StudentPassword AND @StudentName=ModuleAssignedToUser;
END
