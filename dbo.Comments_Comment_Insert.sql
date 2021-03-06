USE [prospect]
GO
/****** Object:  StoredProcedure [dbo].[Comments_Comment_Insert]    Script Date: 3/2/2020 8:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Comments_Comment_Insert] 
	@commentText nvarchar(4000),
		@tableKeyId int,
		@tableName nvarchar(50),	
		@parentCommentId int = null,
		@createdById int,	
		@modifiedById int,
		@id int Output
		
	/* ---TEST CODE----------
	Declare @testid int = 22
	
	exec dbo.Comments_Comment_Insert
		@commentText="Cool",
		@tablekeyId=14,
		@tableName="Events_ProspectEvent",
		@parentCommentId= null,
		@createdById=7,
		@modifiedById=7,
		@id=@testid 

		select @testid

		exec dbo.Comments_Comment_SelectAll

		 
	*/
		
		
AS
BEGIN
	
	SET NOCOUNT ON;

	insert into dbo.Comments_Comment([CommentText], [TableKeyId], [TableName], [ParentCommentId], [CreatedById], [ModifiedById])
		values(@commentText, @tablekeyId, @tableName, @parentCommentId, @createdById, @modifiedById)

		set @id = SCOPE_IDENTITY()



END
