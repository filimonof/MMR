CREATE TRIGGER [TR_tblPPBR_Parameters_INSERT]
ON [dbo].[tblPPBR_Parameters]
FOR INSERT
AS 
BEGIN
	SET NOCOUNT ON	

	IF @@ROWCOUNT > 1
	BEGIN
		RAISERROR ('Нельзя добавлять несколько значений сразу', 16, 1)
		GOTO QuitWithRollback
	END

	-- при инсерт указывается только Name
	
	IF NOT EXISTS(SELECT * FROM deleted)
	BEGIN
		IF NOT EXISTS(SELECT * FROM tblPPBR_Parameters p JOIN inserted i ON p.ID <> i.ID WHERE p.[Enabled] = 1) 
		BEGIN
			-- если первая активная запись то полюбому метку ставим временной
			UPDATE tblPPBR_Parameters
				SET [Order] = 1, IsLabelTime = 1
				FROM inserted 
				WHERE inserted.ID = tblPPBR_Parameters.ID 
		END
		ELSE
		BEGIN
			-- order ставится попорядку
			UPDATE tblPPBR_Parameters
				SET [Order] = 1 + (SELECT MAX([Order]) FROM tblPPBR_Parameters WHERE [Enabled] = 1)
				FROM inserted 
				WHERE inserted.ID = tblPPBR_Parameters.ID 
		END
	END	

GOTO   Quit

QuitWithRollback:
	IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION 

Quit: 
			
END;