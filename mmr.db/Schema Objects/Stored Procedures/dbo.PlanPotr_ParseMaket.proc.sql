CREATE PROCEDURE [dbo].[PlanPotr_ParseMaket]
(
	@maket xml
)
AS
BEGIN
	
	BEGIN TRANSACTION
	
	DECLARE @idoc INT,
			@date NCHAR(8),		    
			@kpo INT,   
			@headerID INT,
			@demInt INT,
			@node NVARCHAR(50),
			@value DECIMAL(18,2),
			@outerData nvarchar(100) 
		
	EXEC sp_xml_preparedocument @idoc OUTPUT, @maket
	
	--разбор заголовка данных
	
	-- <LocationForecasts>
    --    <LocationForecast DemDate="20110708" LocationKPO="316770">
    --       ...
    --    <LocationForecast
    --</LocationForecast>
    DECLARE locationForecasts CURSOR FOR     
		SELECT [date], [kpo]
    		FROM OPENXML (@idoc, N'//LocationForecast')
    		WITH 
			(
				[date] NCHAR(8) 'attribute::DemDate',
				[kpo] INT 'attribute::LocationKPO'
			)
	OPEN locationForecasts
	FETCH NEXT FROM locationForecasts INTO @date, @kpo
	WHILE (@@fetch_status <> -1)
	BEGIN
		IF (@@fetch_status <> -2)
		BEGIN
			--преобразовать date не нада
			
			
			-- есть данные , сохранить заголовок
			IF EXISTS (SELECT * FROM [tblPlanPotr_HeaderMaket] WHERE [DateMaket]=@date AND [LocationKPO]=@kpo)
			BEGIN
				UPDATE [tblPlanPotr_HeaderMaket] 
				SET [DateEvent] = GETDATE()
				WHERE [DateMaket]=@date AND [LocationKPO]=@kpo
				--comment ?				
				SELECT TOP 1 @headerID=[ID] FROM [tblPlanPotr_HeaderMaket] WHERE [DateMaket]=@date AND [LocationKPO]=@kpo				
			END
			ELSE
			BEGIN
				INSERT INTO [tblPlanPotr_HeaderMaket]([DateMaket], [LocationKPO])
				VALUES (@date, @kpo)
				--comment ?
				SET @headerID = @@IDENTITY
			END			
			
			-- цикл по вложенным значениям
			
			-- <LocationForecast DemDate="20110708" LocationKPO="316770">
			--   <Values>
            --     <Value DemInt="1">257</Value> 
            --     ...
			--     <Value DemInt="24">293</Value> 
			--   </Values>
			--   <SumValues>
			--     <Value DemInt="1">257</Value> 
			--     ...
			--   </SumValues>			 
			
			set @outerData = N'//LocationForecast[attribute::DemDate='''+ CAST(@date AS NVARCHAR(8))+N''' and attribute::LocationKPO=''' + CAST(@kpo AS NVARCHAR(50))+  N''']/*/Value'
			--N'//LocationForecast/*/Value'
			DECLARE valuesCursor CURSOR FOR     
				SELECT [demInt], [node], [value]				
    				FROM OPENXML (@idoc, @outerData)    				
    				WITH 
					(
						[demInt] INT 'attribute::DemInt',
						[node] NVARCHAR(50) '@mp:parentlocalname',
						[value] DECIMAL(18,2) 'text()'
					)
			OPEN valuesCursor
			FETCH NEXT FROM valuesCursor INTO @demInt, @node, @value			
			WHILE (@@fetch_status <> -1)
			BEGIN
				IF (@@fetch_status <> -2)
				BEGIN	
					-- есть данные , сохранить 
					IF EXISTS (SELECT * FROM [tblPlanPotr_DataMaket] WHERE [HeaderID]=@headerID AND [Node] = @node AND [DemInt] = @demInt)
					BEGIN
						UPDATE [tblPlanPotr_DataMaket] 
						SET [Value] = @value
						WHERE [HeaderID] = @headerID AND [Node] = @node AND [DemInt] = @demInt
					END
					ELSE
					BEGIN
						INSERT INTO [tblPlanPotr_DataMaket]([HeaderID], [Node], [DemInt], [Value])
						VALUES (@headerID, @node, @demInt, @value)
					END				
				END
				FETCH NEXT FROM valuesCursor INTO @demInt, @node, @value			
			END
			CLOSE valuesCursor
			DEALLOCATE valuesCursor
				
				
			
		END	
		FETCH NEXT FROM locationForecasts INTO @date, @kpo
	END
	CLOSE locationForecasts
	DEALLOCATE locationForecasts
	
	---ROLLBACK TRANSACTION
	
	EXEC sp_xml_removedocument @idoc

	COMMIT TRANSACTION	

END
