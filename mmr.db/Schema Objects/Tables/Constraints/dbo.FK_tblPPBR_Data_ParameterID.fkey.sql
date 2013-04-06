ALTER TABLE [dbo].[tblPPBR_Data] ADD
	CONSTRAINT [FK_tblPPBR_Data_ParameterID] 
		FOREIGN KEY ([ParameterID]) 
		REFERENCES [dbo].[tblPPBR_Parameters] ([ID]) 
		ON UPDATE  NO ACTION 
		ON DELETE CASCADE;


