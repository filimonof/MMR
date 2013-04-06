CREATE TABLE [dbo].[tblPlanPotr_DataMaket]
(
	[ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 	
	[HeaderID] INT NOT NULL,
	[Node] NVARCHAR(50) NOT NULL,
	[DemInt] INT NOT NULL,  	
	[Value] DECIMAL(18,2) NULL	
);
/*
Данные макета прогноза потребления по территории
HeaderID       заголовок макета - reference tblPlanPotr_HeaderMaket.ID
DemInt         Временная метка 
Value          Значение

UNIQUE HeaderID Node DemInt  - [IX_tblPlanPotr_DataMaket_HeaderIDNodeDemInt]
[FK_tblPlanPotr_DataMaket_HeaderID]
*/
