CREATE TABLE [dbo].[tblPPBR_Data]
(
	[ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 	
	[LogID] INT NOT NULL,
	[TimeID] INT NOT NULL,  
	[ParameterID] INT NOT NULL,		
	[Value] DECIMAL(18,2) NULL	
);
/*
Данные макета ППБР 24
LogID          какому событию пр надлежат данные - reference tblPPBR_Logs.ID
TimeID         Время - reference tblPPBR_Time.ID
ParameterID    Парметр-reference tblPPBR_Parameters.ID
Value          Значение

INDEX  LogID
UNIQUE LogID ParameterID TimeID 

*/
