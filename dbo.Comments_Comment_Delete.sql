USE [prospect]
GO
/****** Object:  StoredProcedure [dbo].[Comments_Comment_Delete]    Script Date: 3/2/2020 8:19:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Comments_Comment_Delete]
	@id int

/*
	exec dbo.Comments_Comment_Delete @id=23
*/

AS
BEGIN
	
	SET NOCOUNT ON;

	Delete dbo.Comments_Comment
		Where Id =@id
		
END
