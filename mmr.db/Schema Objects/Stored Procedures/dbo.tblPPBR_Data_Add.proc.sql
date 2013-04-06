CREATE PROCEDURE [dbo].[tblPPBR_Data_Add]
(
	@logID INT,
	@timeID INT,
	@param VARCHAR(MAX),
	@value VARCHAR(MAX)
)
AS
BEGIN
	DECLARE @delimeter VARCHAR(10), @len_delimeter INT
	SET @delimeter = ' '	
	SET @len_delimeter = 1

	SET @param = LTRIM(RTRIM(@param))
	SET @value = LTRIM(RTRIM(@value))

	DECLARE @p INT, @v DECIMAL(18, 2)
	DECLARE @i INT, @to INT, @i2 INT, @to2 INT
	SET @i = 0	
	SET @i2 = 0	
	
	WHILE @i <= LEN(@param)
	BEGIN	
		SET @to = CHARINDEX(@delimeter, @param, @i)
		SET @to2 = CHARINDEX(@delimeter, @value, @i2)
		
		IF @to = 0 SET @to = LEN(@param)+1
		IF @to2 = 0 SET @to2 = LEN(@value)+1
				
		SET @p = CAST(SUBSTRING(@param, @i, @to-@i) AS INT)
		SET @v = CAST(SUBSTRING(@value, @i2, @to2-@i2) AS DECIMAL(18,2))
		
		INSERT INTO tblPPBR_Data(LogID, TimeID, ParameterID, Value) VALUES (@logID, @timeID, @p, @v)	
		
		SET @i = @to + @len_delimeter
		SET @i2 = @to2 + @len_delimeter
	END
END
