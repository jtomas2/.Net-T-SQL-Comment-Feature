using Prospect.Models.Domain.Comments;
using Prospect.Models.Requests.Comments;
using Prospect.Services.Interfaces.Comments;
using Prospect.Services.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Services.Comments
{
    public class CommentVoteService: BaseService, ICommentVoteService
    {
        public List<CommentVote> GetAll()
        {
            List<CommentVote> list = new List<CommentVote>();

            DataProvider.ExecuteCmd("dbo.Comments_CommentVote_SelectAll",
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    list.Add(DataMapper<CommentVote>.Instance.MapToObject(reader));
                });
            return list;
        }
        
        public void Post(CommentVoteAddRequest model)
        {
           

            DataProvider.ExecuteNonQuery("dbo.Comments_CommentVote_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@commentId", model.CommentId);
                    paramCollection.AddWithValue("@upvote", model.Upvote);
                    paramCollection.AddWithValue("@downvote", model.Downvote);
                    paramCollection.AddWithValue("createdById", model.CreatedById);
                }
               );
            
        }
        public void Put(CommentVoteAddRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Comments_CommentVote_Update",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@upvote", model.Upvote);
                    paramCollection.AddWithValue("@downvote", model.Downvote);
                    paramCollection.AddWithValue("@commentId", model.CommentId);
                });
        }
        public CommentVote GetById(int commentId)
        {
            CommentVote commentVote = new CommentVote();
            DataProvider.ExecuteCmd("dbo.Comments_CommentVote_SelectById",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@CommentId", commentId);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    commentVote = (DataMapper<CommentVote>.Instance.MapToObject(reader));
                }
                );
            return commentVote;
        }
        public void Delete(int commentId)
        {
            DataProvider.ExecuteNonQuery("dbo.Comments_CommentVote_Delete",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@CommentId", commentId);
                });
        }
    }
}
