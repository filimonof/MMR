ALTER TABLE [dbo].[tblPPBR_CKParameters]
	ADD CONSTRAINT [FK_tblPPBR_CKParameters_ParameterID] 
	FOREIGN KEY ([ParameterID])
	REFERENCES [dbo].[tblPPBR_Parameters] ([ID]) 		
		ON UPDATE  NO ACTION 
		ON DELETE CASCADE;

