CREATE PROCEDURE [dbo].[tblPPBR_Logs_Add]
(
	@dt DATETIME, 
	@status INT = 0, 
	@comment NVARCHAR(MAX) = NULL,
	@id INT OUTPUT
)
AS
BEGIN	
	INSERT INTO tblPPBR_Logs (DateMaket, StatusID, Comment) VALUES (@dt, @status, @comment)
	SET @id = SCOPE_IDENTITY()
END