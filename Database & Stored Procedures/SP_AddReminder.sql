CREATE PROCEDURE SP_AddReminder(
@ModuleCodeOfModuleToStudy VARCHAR (50) = '',
@ModuleToStudy VARCHAR (50) ='',
@DateSetToStudy DATE =''
)
AS
BEGIN

INSERT INTO Reminder 
VALUES(@ModuleCodeOfModuleToStudy,@ModuleToStudy,@DateSetToStudy)


END
