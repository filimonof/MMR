CREATE UNIQUE INDEX [IX_tblPPBR_Parameters_OrderEnabled]
	ON [dbo].[tblPPBR_Parameters]
	(
		[Order], [Enabled]
	);
