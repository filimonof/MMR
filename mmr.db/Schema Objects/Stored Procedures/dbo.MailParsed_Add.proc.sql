CREATE PROCEDURE [dbo].[MailParsed_Add]
(
	@MailID NVARCHAR(100) 	
)
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM tblMailParsed WHERE [MailID] = @MailID)	
		INSERT INTO tblMailParsed ([MailID]) VALUES (@MailID)
END