CREATE TABLE [dbo].[tblPPBR_CKParameters]
(
	[ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 	
	[Codename] NVARCHAR(50) NOT NULL UNIQUE,
	[ParameterID] INT NOT NULL,
	[OnlyHourse] BIT NOT NULL DEFAULT ((1)),		
	[ControlSumma] BIT NOT NULL DEFAULT ((0))
);
/*
Параметры макета СК-2003
ID             ID
Codename       Имя - код параметра
ParameterID    Парметр-reference tblPPBR_Parameters.ID
AllOrHourse    0(false) - получасовые значения ; 1(true) - часовые
ControlSumma   Ставить ли в конце данных проверочную сумму

INDEX  ID
UNIQUE Codename
*/