SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE InteractsSP
	@timestamp datetime
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION
	INSERT INTO Interacts(timestamp) VALUES (@timestamp);
	IF @@ERROR = 0
		COMMIT
	ELSE
		ROLLBACK
END
GO
