USE [prospect]
GO
/****** Object:  StoredProcedure [dbo].[Comments_Comment_SelectAll]    Script Date: 3/2/2020 8:33:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Comments_Comment_SelectAll] 

	/*
	
		exec dbo.Comments_Comment_SelectAll
	
	*/
	
	AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT [Id], [CommentText], [TableKeyId], [CreatedDate],[CreatedById], [ModifiedDate],[ModifiedById]
	from dbo.Comments_Comment
	
END
