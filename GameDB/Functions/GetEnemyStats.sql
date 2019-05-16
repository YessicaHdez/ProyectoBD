SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION GetEnemyStats
(
	@Id nchar(40)
)
RETURNS 
@Tab TABLE 
(
	damage int,
	health int
)
AS
BEGIN
	DECLARE @damage int = 0, @health int = 0;
	SELECT @damage = Damage, @health = Health FROM Enemy WHERE ID = @Id;
	INSERT INTO @Tab (damage, health) VALUES (@damage, @health);
	
	RETURN 
END
GO