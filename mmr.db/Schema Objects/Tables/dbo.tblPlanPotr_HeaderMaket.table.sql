CREATE TABLE [dbo].[tblPlanPotr_HeaderMaket]
(
	[ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 		
	[DateMaket] DATETIME NOT NULL,
	[LocationKPO] INT NOT NULL,
	[DateEvent] DATETIME NOT NULL DEFAULT GETDATE(),
	[Comment] NVARCHAR(MAX) NULL
);

/*
Заголовок файла макета получаемого с сайта http://market.odusv.so-cdu.ru/main.aspx?P=30 
прогноз потребления по территории

DateMaket     дата макета
LocationKPO   номер КПО
DateEvent     дата записи макета в базу
Comment       коментарий, может содержать имя записавшего 

[IX_tblPlanPotr_HeaderMaket_DateMaketLocationKPO]
*/