CREATE PROCEDURE [dbo].[tblPPBR_Data_CreateMaketPPBR]
(
	@logID INT 	
) 
AS
BEGIN
	SET NOCOUNT ON	

	DECLARE @ID INT
	DECLARE @IsLabelTime BIT
	DECLARE @cols NVARCHAR(MAX)
	SET @cols =  N''
	DECLARE @header NVARCHAR(MAX)
	SET @header =  N''

	DECLARE ColsCursor CURSOR FOR	
		SELECT [ID], IsLabelTime
		FROM tblPPBR_Parameters 
		WHERE [Enabled] = 1 
		ORDER BY [Order]
	OPEN ColsCursor
	FETCH NEXT FROM ColsCursor INTO @ID, @IsLabelTime
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF @IsLabelTime = 1 
			SET @header = @header + N',t.Time AS [' + CAST(@ID AS nvarchar(10)) + N']' 
		ELSE
			SET @header = @header + N',[' + CAST(@ID AS nvarchar(10)) + N']'
		
		SET @cols = @cols + N',[' + CAST(@ID AS nvarchar(10)) + N']'
		
		FETCH NEXT FROM ColsCursor INTO @ID, @IsLabelTime
	END
	CLOSE ColsCursor
	DEALLOCATE ColsCursor	
	
	IF LEN(@cols) = 0
	BEGIN
		RAISERROR ('Нет параметров', 16, 1)
		GOTO QuitWithRollback
	END
	
	-- убираем первую запятую
	SET @cols = SUBSTRING(@cols, 2, LEN(@cols))
	SET @header = SUBSTRING(@header, 2, LEN(@header))
	--set @cols =  '[11], [12], [13], [14], [15], [16], [17], [18]'
	--set @header =  't.Time AS [11], [12], [13], [14], [15], [16], [17], [18]'

	DECLARE @sql NVARCHAR(MAX)
	SET @sql = N'SELECT ' + @header + ' 
	 FROM ( 
		SELECT  TimeID, ParameterID, [Value]
		FROM dbo.tblPPBR_Data 
		WHERE LogID = ' + CAST(@logID AS nvarchar(10)) + '
	) AS data PIVOT (
		SUM ( [Value] ) 
		FOR ParameterID IN (' + @cols + ' )	
	) AS pivot_data JOIN dbo.tblTime t ON pivot_data.[TimeID] = t.ID
	ORDER BY TimeID';
	EXEC(@sql)
	
GOTO   Quit

QuitWithRollback:
	IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION 

Quit: 	
	
END