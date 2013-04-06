CREATE TRIGGER [dbo].[TR_tblPPBR_Parameters_UPDATE_BEFORE]
ON [dbo].[tblPPBR_Parameters]
INSTEAD OF UPDATE 
AS 
BEGIN	
	SET NOCOUNT ON
	
	IF @@ROWCOUNT > 1
	BEGIN
		RAISERROR ('Нельзя добавлять несколько значений сразу', 16, 1)
		GOTO QuitWithRollback  		
	END
	
	IF EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted)
	BEGIN	
		DECLARE @ID INT
		SELECT @ID = ID FROM inserted

-- IsLabelTime
		IF UPDATE([IsLabelTime])
		BEGIN		
			IF EXISTS(SELECT TOP 1 * FROM tblPPBR_Data d JOIN inserted i ON d.ParameterID = i.ID)
			BEGIN
				RAISERROR ('Этот параметр не может быть временной меткой, т.к. на него есть данные', 16, 1)
				GOTO QuitWithRollback  
			END					
			
			IF EXISTS(SELECT * FROM inserted WHERE IsLabelTime = 0) 
				AND EXISTS(SELECT * FROM deleted WHERE IsLabelTime = 1)
			BEGIN
				RAISERROR ('Должна быть одна метка времени', 16, 1)
				GOTO QuitWithRollback  
			END

			IF EXISTS(SELECT * FROM inserted WHERE Enabled = 0) 				
			BEGIN
				RAISERROR ('Нельзя архивный параметр сделать меткой времени', 16, 1)
				GOTO QuitWithRollback  
			END

			-- если кто-то становится меткой времени то активные метки снимаются
			IF EXISTS(SELECT * FROM inserted WHERE IsLabelTime = 1)
			BEGIN
				UPDATE tblPPBR_Parameters
					SET IsLabelTime = 0
					FROM inserted 
					WHERE inserted.ID <> tblPPBR_Parameters.ID 		

				UPDATE tblPPBR_Parameters
					SET IsLabelTime = 1
					FROM inserted 
					WHERE inserted.ID = tblPPBR_Parameters.ID 		
			END	
			
		END

-- Name
		IF UPDATE([Name]) 
		BEGIN
			-- обработку этого события отдаем в тригер AFTER UPDATE
			-- а он отдаст на усмотрение UNIQUE		
			UPDATE tblPPBR_Parameters
				SET tblPPBR_Parameters.[Name] = inserted.[Name]
				FROM inserted 
				WHERE inserted.ID=tblPPBR_Parameters.ID 
		END

-- Enabled
		IF UPDATE(Enabled) 
		BEGIN		
			IF EXISTS(SELECT * FROM inserted WHERE IsLabelTime = 1) 				
			BEGIN
				RAISERROR ('Метки времени выводить из работы нельзя', 16, 1)
				GOTO QuitWithRollback  
			END		
			DECLARE @maxOrder INT			
			IF EXISTS(SELECT * FROM inserted WHERE Enabled = 1)
			BEGIN
				SELECT @maxOrder = MAX([Order]) FROM tblPPBR_Parameters WHERE [Enabled] = 1
				IF @maxOrder IS NULL SET @maxOrder = 0
				-- вводится в работу, ставим в конец
				UPDATE tblPPBR_Parameters
					SET [Order] = @maxOrder + 1,
						Enabled = 1
					FROM inserted 
					WHERE inserted.ID = tblPPBR_Parameters.ID 				
			END
			ELSE
			BEGIN
				SELECT @maxOrder = MAX([Order]) FROM tblPPBR_Parameters WHERE [Enabled] = 0
				IF @maxOrder IS NULL SET @maxOrder = 0
				-- выводится в архив, ставим в конец
				UPDATE tblPPBR_Parameters
					SET [Order] = @maxOrder + 1,
						Enabled = 0
					FROM inserted 
					WHERE inserted.ID = tblPPBR_Parameters.ID 		
			END
			--Перещитываем параметр order чтобы всегда активные параметры были попорядку
			UPDATE tblPPBR_Parameters 
			SET [Order] = pNum.Num
			FROM (
				SELECT COUNT(*) as Num, p1.[Name]
				FROM (SELECT * FROM tblPPBR_Parameters WHERE Enabled=1) p1 
					JOIN (SELECT * FROM tblPPBR_Parameters WHERE Enabled=1) p2 
					ON p1.[Order]>=p2.[Order]				
				GROUP BY p1.[Name]
				) pNum 
			WHERE pNum.[Name] = tblPPBR_Parameters.[Name]
		END

-- Order
		IF UPDATE([Order]) 
		BEGIN
			DECLARE @newOrder INT			
			DECLARE @oldOrder INT
			DECLARE @enabled INT
			SELECT @newOrder = [Order] FROM inserted			
			SELECT @oldOrder = [Order] FROM deleted
			SELECT @enabled = [Enabled] FROM deleted

			IF (@newOrder > @oldOrder) AND  @ID = (SELECT TOP 1 ID FROM tblPPBR_Parameters WHERE Enabled = @enabled ORDER BY [Order] DESC)
			BEGIN
				RAISERROR ('Это последний параметр', 16, 1)
				GOTO QuitWithRollback  
			END			
			IF (@newOrder < @oldOrder) AND  @ID = (SELECT TOP 1 ID FROM tblPPBR_Parameters WHERE Enabled = @enabled ORDER BY [Order] ASC)
			BEGIN
				RAISERROR ('Это первый параметр', 16, 1)
				GOTO QuitWithRollback  				
			END	
			IF EXISTS(SELECT * FROM tblPPBR_Parameters WHERE Enabled = @enabled AND [Order] = @newOrder )
			BEGIN
				UPDATE tblPPBR_Parameters
					SET tblPPBR_Parameters.[Order] = NULL
					FROM inserted 
					WHERE inserted.ID=tblPPBR_Parameters.ID 

				UPDATE tblPPBR_Parameters
					SET [Order] = @oldOrder
					WHERE [Order] = @newOrder
			END
			UPDATE tblPPBR_Parameters
				SET tblPPBR_Parameters.[Order] = inserted.[Order]
				FROM inserted 
				WHERE inserted.ID=tblPPBR_Parameters.ID 

		END --IF UPDATE([Order])

	END	--IF EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted)
 
GOTO   Quit

QuitWithRollback:
	IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION 

Quit: 

END;
