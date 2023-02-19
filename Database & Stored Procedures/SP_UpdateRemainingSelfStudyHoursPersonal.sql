CREATE PROCEDURE SP_UpdateRemainingSelfStudyHoursPersonal(
@ModuleCode VARCHAR (50) ='',
@RemainingSelfStudyHours INT = 0
)
AS
BEGIN
UPDATE PersonalUserModules
SET RemainingSelfStudyHours= RemainingSelfStudyHours - @RemainingSelfStudyHours
WHERE @ModuleCode=ModuleCode
END

