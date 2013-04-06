CREATE PROCEDURE [dbo].[tblPPBR_Logs_Update]
(
	@id INT, 
	@status INT = 0,
	@comment NVARCHAR(MAX) = NULL
)
AS
BEGIN
	IF EXISTS(SELECT * FROM tblPPBR_Logs WHERE [ID] = @id)
		UPDATE tblPPBR_Logs SET StatusID = @status, Comment = @comment WHERE [ID] = @id
END