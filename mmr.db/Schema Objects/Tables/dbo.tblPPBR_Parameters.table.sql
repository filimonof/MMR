CREATE TABLE [dbo].[tblPPBR_Parameters]
(
	[ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[Name] NVARCHAR(50) NOT NULL UNIQUE,
	[Order] INT NULL,
    [Enabled] BIT NOT NULL DEFAULT ((1)),
    [IsLabelTime] BIT NOT NULL DEFAULT ((0))
);
/*
Параметры макета ППБР 24
Name          Имя
Order         Порядковый номер в макете 1,2,3,..  NULL  - в архивe
Enabled       0-отключен, 1-в работе; если удалить то и данные удаляться  
Order-Enabled уникальное сочетание
IsLabelTime   это поле означает метку времени, должна быть хотя бы одна

TR_tblPPBR_Parameters_ChangeLabelTime

*/

