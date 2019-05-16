SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION LastPosition
(
	@PlayerName nchar(40)
)
RETURNS 
@Tab TABLE 
(
	x int,
	y int
)
AS
BEGIN
	DECLARE @X int = -50, @y int = 0;
	SELECT TOP 1 @X = X, @y = Y FROM Position JOIN Player ON Player.ID = Position.IDPlayer
		WHERE Player.Name = @PlayerName ORDER BY timeststamp DESC;
	INSERT INTO @Tab (x, y) VALUES (@X, @Y);
	
	RETURN 
END
GO