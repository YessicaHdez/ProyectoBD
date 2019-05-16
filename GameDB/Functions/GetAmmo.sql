SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION GetAmmo
(	
	@PlayerName nchar(40)
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT IDAmmo, CurrentAmmo, UsedAmmo FROM Player JOIN Has ON Player.ID = Has.IDplayer
		WHERE Name = @PlayerName
)
GO
