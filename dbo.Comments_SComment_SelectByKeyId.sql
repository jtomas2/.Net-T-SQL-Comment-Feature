USE [prospect]
GO
/****** Object:  StoredProcedure [dbo].[Comments_SComment_SelectByKeyId]    Script Date: 3/3/2020 9:38:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Comments_SComment_SelectByKeyId] 
@tableKeyId int,
@tableName nvarchar(50),
@sortBy nvarchar(20),
@userBaseId int,
@pageNumber int,
@perPageAmt int = 10

/*
	
	exec dbo.Comments_SComment_SelectByKeyId
	@tableKeyId = 166,
	@tableName = "Blogs_Blog",
	@sortBy = "newestFirst",
	@userBaseId = 8,
	@pageNumber = 2

*/

AS
BEGIN
	declare @TotalPages int = CEILING((SELECT cast(COUNT(comments.CommentText) as float) from Comments_Comment comments
	left join Comments_CommentVote vote on vote.CommentId=comments.Id
	where comments.TableKeyId=@tableKeyId and comments.TableName=@tableName and comments.ParentCommentId is null) / @perPageAmt)

	declare @TotalComments int = (select count(*) from Comments_Comment comments
	left join Comments_CommentVote vote on vote.CommentId=comments.Id
where comments.TableKeyId=@tableKeyId and comments.TableName=@tableName)

;WITH specificComments

as
(
select 
	ISNULL(sum(vote.Upvote),0) as UpVote, 
	ISNULL(sum(vote.DownVote),0) as DownVote,
	comments.Id
from Comments_Comment comments
	left join Comments_CommentVote vote on vote.CommentId=comments.Id
where comments.TableKeyId=@tableKeyId and comments.TableName=@tableName and comments.ParentCommentId is null
group by comments.Id
)

select
	--case when @userBaseId=CreatedById then 1 else 0 end as IsAuthor,
	allComments.Id,
	allComments.CommentText,
	allComments.TableKeyId,
	allComments.TableName,
	allComments.CreatedDate,
	allComments.CreatedById,
	allComments.ModifiedDate,
	comments.UpVote, 
	comments.DownVote,
	users.FirstName,
	users.LastName,
	users.AvatarUrl,
	@TotalComments as TotalComments,
	@TotalPages as TotalPages
from specificComments comments
	inner join Comments_Comment allComments 
on 
	comments.Id = allComments.Id
	inner join Users_UserProfile users
on 
	allComments.CreatedById = users.UserBaseId
where 
	tableKeyId = @tableKeyId and
	tableName = @tableName
	ORDER BY 
	CASE WHEN @sortBy = 'topComments' THEN comments.UpVote - comments.DownVote end DESC,
	CASE WHEN @sortBy = 'newestFirst' THEN allComments.CreatedDate end DESC
	offset 0 ROWS FETCH NEXT @perPageAmt * @pageNumber rows only;

	if(OBJECT_ID('tempdb..#replies') is not null) drop table #replies

	select * 
	into #replies
	from comments_comment 
	where ParentCommentId is not null and tableName = @tableName and TableKeyId = @tableKeyId

	--select * from #replies

	;with RepliesCountsCTE as (
	select 
		ISNULL(sum(vote.Upvote),0) as UpVote, 
		ISNULL(sum(vote.DownVote),0) as DownVote,
		replies.Id
	from #replies as replies
	left join Comments_CommentVote vote on vote.CommentId=replies.Id
	group by replies.Id
	)

	select 
		--case when @userBaseId=CreatedById then 1 else 0 end as IsAuthor,
		r.Id,
		r.CommentText ,
		r.TableKeyId,
		r.ParentCommentId,
		r.TableName,
		r.CreatedDate,
		r.CreatedById,
		r.ModifiedDate,
		cte.UpVote, 
		cte.DownVote,
		users.FirstName,
		users.LastName,
		users.AvatarUrl 
	from RepliesCountsCTE cte
	inner join #replies r on r.Id=cte.Id
	inner join Users_UserProfile users on r.CreatedById = users.UserBaseId

END
