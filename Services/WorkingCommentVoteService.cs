using Prospect.Models.Requests.Comments;
using Prospect.Services.Interfaces.Comments;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Services.Comments
{
    public class WorkingCommentVoteService : BaseService, IWorkingCommentVoteService
    {
        public void Put(WorkingCommentVoteUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Comments_WorkingCommentVote",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UpVote", model.UpVote);
                    paramCollection.AddWithValue("@DownVote", model.DownVote);
                    paramCollection.AddWithValue("@UserBaseId", model.UserBaseId);
                    paramCollection.AddWithValue("@CommentId", model.CommentId);
                    paramCollection.AddWithValue("@Action", model.Action);
                });
        }
    }
}
