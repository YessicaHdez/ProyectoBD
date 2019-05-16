SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE RegisterUser
	@PlayerName nchar(40)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION
	DECLARE @PlayerId int;
	SELECT @PlayerId = ID FROM Player WHERE Name = @PlayerName;
	IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Error int = 0;
		INSERT INTO Player(Name, Score) VALUES(@PlayerName, 0);
		SELECT @Error = @Error + @@ERROR;
		EXEC InitializeAmmo @PlayerName;
		IF @Error = 0
			COMMIT
		ELSE
			ROLLBACK
	END
END
GO
