CREATE PROCEDURE [dbo].[MailParsed_IsExists]
(
	@MailID NVARCHAR(100) 	
)
AS
BEGIN		
	DECLARE @ID INT
	SELECT TOP 1 @ID = ID FROM tblMailParsed WHERE [MailID] = @MailID ORDER BY DateParse
	IF @ID IS NULL 
		RETURN 0 
	ELSE 
		RETURN @ID
END