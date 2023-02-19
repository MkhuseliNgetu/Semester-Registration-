CREATE PROCEDURE SP_UpdateRemainingSelfStudyHours(
@ModuleCode VARCHAR (50) ='',
@RemainingSelfStudyHours INT = 0
)
AS
BEGIN
UPDATE AllModules
SET RemainingSelfStudyHours=RemainingSelfStudyHours - @RemainingSelfStudyHours
WHERE @ModuleCode=ModuleCode
END
