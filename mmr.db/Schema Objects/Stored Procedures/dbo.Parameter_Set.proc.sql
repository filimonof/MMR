CREATE PROCEDURE [dbo].[Parameter_Set]
(
	@name NVARCHAR(50), 
	@value NVARCHAR(MAX)
)
AS
BEGIN
	IF EXISTS(SELECT * FROM tblParameters WHERE [Name] = @name)
		UPDATE tblParameters SET [Value] = @value WHERE [Name] = @name
	ELSE
		INSERT INTO tblParameters ([Name], [Value]) VALUES (@name, @value)	
END;