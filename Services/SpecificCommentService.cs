using Prospect.Models.Domain.Comments;
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
    public class SpecificCommentService : BaseService, ISpecificCommentService
    {
        public List<SpecificComment> GetByKeyId(int tableKeyId, string tableName, int pageNumber, string sortBy, int userBaseId)
        {
            List<SpecificComment> list = new List<SpecificComment>();
            List<SpecificComment> replyList = new List<SpecificComment>();
            Dictionary<int, List<SpecificComment>> replies = new Dictionary<int, List<SpecificComment>>();

            DataProvider.ExecuteCmd("dbo.Comments_SComment_SelectByKeyId",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@tableKeyId", tableKeyId);
                    paramCollection.AddWithValue("@tableName", tableName);
                    paramCollection.AddWithValue("@sortBy", sortBy);
                    paramCollection.AddWithValue("@pageNumber", pageNumber);
                    paramCollection.AddWithValue("@userBaseId", userBaseId);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    if (set == 0)
                    {
                        list.Add(DataMapper<SpecificComment>.Instance.MapToObject(reader));
                    }
                    else
                    {
                        replyList.Add(DataMapper<SpecificComment>.Instance.MapToObject(reader));
                    }
                });

            foreach (SpecificComment reply in replyList)
            {
                if (replies.ContainsKey(reply.ParentCommentId))
                {
                    replies[reply.ParentCommentId].Add(reply);
                }
                else
                {
                    List<SpecificComment> newReply = new List<SpecificComment>();
                    newReply.Add(reply);
                    replies.Add(reply.ParentCommentId, newReply);
                }
            }

            AddReplies(list, replies);

            return list;
        }

        private List<SpecificComment> AddReplies(List<SpecificComment> parents, Dictionary<int, List<SpecificComment>> replies)
        {
            if (parents != null)
            {
                foreach (SpecificComment parent in parents)
                {
                    if (replies.ContainsKey(parent.Id))
                    {
                        parent.Replies = replies[parent.Id];
                        replies.Remove(parent.Id);
                    }
                }

                if (replies.Count > 0)
                {
                    foreach (SpecificComment parent in parents)
                    {
                        AddReplies(parent.Replies, replies);
                    }
                }
            }
            return parents;
        }
    }
}

