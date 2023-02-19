USE MASTER;
CREATE DATABASE ModuleStorage;
USE ModuleStorage;

CREATE TABLE AllModules (
ModuleCode VARCHAR (50) NOT NULL PRIMARY KEY,
ModuleName VARCHAR(150) NOT NULL,
ModuleCredits INT NOT NULL,
ModuleClassHours INT NOT NULL,
ModuleSelfStudyHours INT NOT NULL,
RemainingSelfStudyHours INT,
);

CREATE TABLE Student (
StudentName VARCHAR(150) NOT NULL PRIMARY KEY,
StudentPassword INT NOT NULL,
);

CREATE TABLE PersonalUserModules(
ModuleCode VARCHAR (50) NOT NULL PRIMARY KEY,
ModuleName VARCHAR(150) NOT NULL,
ModuleCredits INT NOT NULL,
ModuleClassHours INT NOT NULL,
ModuleSelfStudyHours INT NOT NULL,
RemainingSelfStudyHours INT,
ModuleAssignedToUser VARCHAR (150) NOT NULL
);
--End Of Task 2 (Revised)

---POE
CREATE TABLE Reminder(
ModuleCodeOfModuleToStudy VARCHAR (50) NOT NULL,
ModuleToStudy VARCHAR (50) NOT NULL,
DateSetToStudy Date NOT NULL
)

