USE [prospect]
GO
/****** Object:  StoredProcedure [dbo].[Comments_Comment_Update]    Script Date: 3/2/2020 8:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Comments_Comment_Update]
	@id int,
		@commentText nvarchar(4000),
		@tablekeyId int,
		@tableName nvarchar(50),
		@createdById int,
		@parentCommentId int = null,
		@modifiedById int

	/*
	Declare @FakeDate datetime2(7) = GetUTCDate()
	exec dbo.Comments_Comment_Update
		@commentTitle="Another awesome title",
		@commentText="The other title for the awesome comment",
		@tablekeyId=0,
		@tableName="awesome name",
		@createdDate=@FakeDate,
		@createdById=0,
		@modifiedDate= @FakeDate,
		@modifiedById=0,
		@id=1

		exec dbo.Comments_Comment_SelectById @id=1

		exec dbo.Comments_Comment_SelectAll
		*/
		

AS
BEGIN
	
	SET NOCOUNT ON;

    Update dbo.Comments_Comment
	Set
	[CommentText]=@commentText, 
	[TableKeyId]= @tablekeyId, 
	[TableName]=@tableName,
	[CreatedById]=@createdById, 
	[ModifiedDate] = GETUTCDATE(),
	[ParentCommentId] = @parentCommentId,
	[ModifiedById]=@modifiedById

	Where id=@id

END
