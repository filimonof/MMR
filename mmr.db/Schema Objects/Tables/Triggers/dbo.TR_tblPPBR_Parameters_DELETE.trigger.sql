CREATE TRIGGER [TR_tblPPBR_Parameters_DELETE]
ON [dbo].[tblPPBR_Parameters]
FOR DELETE 
AS 
BEGIN
	SET NOCOUNT ON
	
	IF @@ROWCOUNT > 1
	BEGIN
		RAISERROR ('Нельзя добавлять несколько значений сразу', 16, 1)
		GOTO QuitWithRollback		
	END
	
	IF NOT EXISTS(SELECT * FROM inserted)
		AND EXISTS(SELECT * FROM deleted WHERE IsLabelTime = 1) 
		AND EXISTS(SELECT * FROM tblPPBR_Parameters) 
	BEGIN
		RAISERROR ('Нельзя удалять временную метку, если есть другие параметры', 16, 1)
		GOTO QuitWithRollback
	END
	
	-- Перещёт order, чтобы было попорядку
	-- работает, ничего не трогать, заебался думать мысль
	UPDATE tblPPBR_Parameters 
	SET [Order] = pNum.Num
	FROM 
		(SELECT COUNT(*) as Num, p1.[Name]
		FROM tblPPBR_Parameters p1 JOIN tblPPBR_Parameters p2 ON p1.[Order] >= p2.[Order]
		WHERE p1.Enabled = 1				
		GROUP BY p1.[Name]) pNum 
	WHERE pNum.[Name] = tblPPBR_Parameters.[Name]

GOTO   Quit

QuitWithRollback:
	IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION 

Quit: 
	
END;
