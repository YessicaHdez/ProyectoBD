SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE KillSP
	@X float,
	@Y float,
	@timestamp datetime
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION
	INSERT INTO Kill1 (X,Y,timestamp) VALUES (@X, @Y, @timestamp)
	IF @@ERROR = 0
		COMMIT
	ELSE
		ROLLBACK
END
GO