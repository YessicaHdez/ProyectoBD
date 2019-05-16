SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE InitializeAmmo
	@PlayerName nchar(40)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION
		DECLARE @PlayerId int;
		SELECT @PlayerId = ID FROM Player WHERE Name = @PlayerName;
		IF @PlayerId IS NOT NULL
		BEGIN
			DECLARE @ErrorCount int = 0, @Current int;
			SELECT @Current = MIN( ID ) FROM Weapon
			
			WHILE @Current IS NOT NULL
			BEGIN
				DECLARE @ID int, @WID int, @DEF int;

				SELECT @ID = Ammo.ID, @WID = Weapon.ID, @DEF = Weapon.DefAmmo 
					FROM Weapon JOIN Ammo ON Weapon.ID_Ammo = Ammo.ID
					WHERE Weapon.ID = @Current;

				INSERT INTO Has (IDAmmo, IDplayer, CurrentAmmo, UsedAmmo) 
					VALUES(@ID, @PlayerId, @DEF, 0);
				SELECT @ErrorCount = @ErrorCount + @@ERROR;

				INSERT INTO Have (IDPlayer, IDWeapon) 
					VALUES(@PlayerId, @WID);
				SELECT @ErrorCount = @ErrorCount + @@ERROR;

				SELECT @Current = MIN( ID ) FROM Weapon WHERE ID > @Current;
			END

			IF @ErrorCount = 0
				COMMIT
			ELSE
				ROLLBACK
		END
		ELSE
		BEGIN
			ROLLBACK
		END
END
GO
