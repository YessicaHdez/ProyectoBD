SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE UpdateAmmo
	@PlayerName nchar(40),
	@AmmoID int,
	@Current int,
	@Used int
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION
	
	DECLARE @PlayerId int;
	SELECT @PlayerId = ID FROM Player WHERE Name = @PlayerName;
	IF @PlayerId IS NOT NULL
	BEGIN
		UPDATE Has SET CurrentAmmo = @Current, UsedAmmo = @Used
			WHERE IDAmmo = @AmmoID AND IDplayer = @PlayerId;
		IF @@ERROR = 0
			COMMIT
		ELSE
			ROLLBACK
	END
END
GO
