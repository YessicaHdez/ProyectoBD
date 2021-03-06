USE [GAME_PROJECT_DB]
GO
/****** Object:  StoredProcedure [dbo].[KillEnemy]    Script Date: 16/05/2019 02:18:25 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[KillEnemy]
	@DId int, 
	@X int,
	@Y int,
	@AmmoId int,
	@PlayerName nchar(40)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION
	DECLARE @PlayerId int, @Score int;

	SELECT @PlayerId = ID FROM Player WHERE Name = @PlayerName

	INSERT INTO Kill1 (X, Y, timestamp, IDDestroy, AmmoId, PlayerId)
		VALUES (@X, @Y, GETDATE(), @DId, @AmmoId, @PlayerId)

	SELECT @Score = Score FROM Player WHERE Name = @PlayerName;

	UPDATE Player SET Score = @Score + 10 WHERE Name = @PlayerName;

	IF @@ERROR = 0
		COMMIT
	ELSE
		ROLLBACK
END
