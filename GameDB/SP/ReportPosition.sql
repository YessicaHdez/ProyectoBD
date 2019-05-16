SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE ReportPosition
	@PlayerName nchar(40),
	@X int,
	@Y int
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION
	
	DECLARE @PlayerId int;
	SELECT @PlayerId = ID FROM Player WHERE Name = @PlayerName;
	IF @PlayerId IS NOT NULL
	BEGIN
		DECLARE @Date datetime;
		SELECT @Date = GETDATE();
		INSERT INTO Position (IDPlayer, X, Y, timeststamp)
			VALUES (@PlayerId, @X, @Y, @Date);
		IF @@ERROR = 0
			COMMIT
		ELSE
			ROLLBACK
	END
END
GO
