CREATE TABLE [dbo].[tblPPBR_Logs]
(
	[ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[DateEvent] DATETIME NOT NULL DEFAULT GETDATE(),
	[DateMaket] DATETIME NOT NULL,
	[StatusID] INT NOT NULL DEFAULT(0),
	[Comment] NVARCHAR(MAX) NULL
);
/*
Журнал событий макета ППБР 24
DateEvent    Дата события
DateMaket    Дата макета
StatusID     состояние собятия - reference tblPPBR_Status.ID

INDEX [DateMaket]

*/