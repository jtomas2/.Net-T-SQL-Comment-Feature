ALTER PROCEDURE [dbo].[Comments_Comment_SelectById]
	@id int
	
	/* 
	
	exec dbo.Comments_Comment_SelectById @id=1

	*/
	
	
AS
BEGIN
	
	SET NOCOUNT ON;

    SELECT [Id], [CommentText], [TableKeyId], [TableName], [CreatedDate], [CreatedById], [ModifiedDate], [ModifiedById] 
		from dbo.Comments_Comment
			where Id=@id
			
END
