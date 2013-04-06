ALTER TABLE [dbo].[tblPlanPotr_DataMaket]
	ADD CONSTRAINT [FK_tblPlanPotr_DataMaket_HeaderID] 
	FOREIGN KEY ([HeaderID])
	REFERENCES [dbo].[tblPlanPotr_HeaderMaket] ([ID])	
		ON UPDATE  NO ACTION 
		ON DELETE CASCADE;
