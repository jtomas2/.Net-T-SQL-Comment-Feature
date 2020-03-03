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
    public class CommentService : BaseService, ICommentService
    {
        public List<Comment> GetAll()
        {
            List<Comment> list = new List<Comment>();

            DataProvider.ExecuteCmd("dbo.Comments_Comment_SelectAll",
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    list.Add(DataMapper<Comment>.Instance.MapToObject(reader));
                });

            return list;
        }
        public int Post(CommentAddRequest model)
        {
            int Id = 0;

            DataProvider.ExecuteNonQuery("dbo.Comments_Comment_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@commentText", model.CommentText);
                    paramCollection.AddWithValue("@tableKeyId", model.TableKeyId);
                    paramCollection.AddWithValue("@tableName", model.TableName);
                    paramCollection.AddWithValue("@createdById", model.CreatedById);
                    paramCollection.AddWithValue("@modifiedById", model.ModifiedById);
                    paramCollection.AddWithValue("@parentCommentId", model.ParentCommentId);
                    SqlParameter paramId = new SqlParameter("@id", SqlDbType.Int);
                    paramId.Direction = ParameterDirection.Output;
                    paramId.Value = Id;
                    paramCollection.Add(paramId);
                },
                returnParameters: delegate (SqlParameterCollection paramCollection)
                {
                    int.TryParse(paramCollection["@id"].Value.ToString(), out Id);
                }
                );
            return Id;
        }
        public void Put(CommentUpdateRequest model)
        {


            DataProvider.ExecuteNonQuery("dbo.Comments_Comment_Update",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@CommentText", model.CommentText);
                    paramCollection.AddWithValue("@TablekeyId", model.TableKeyId);
                    paramCollection.AddWithValue("@TableName", model.TableName);
                    paramCollection.AddWithValue("@CreatedById", model.CreatedById);
                    paramCollection.AddWithValue("@parentCommentId", model.ParentCommentId);
                    paramCollection.AddWithValue("@ModifiedById", model.ModifiedById);
                    paramCollection.AddWithValue("@Id", model.Id);
                });

        }
        public Comment GetById(int id)
        {
            Comment comment = new Comment();
            DataProvider.ExecuteCmd("dbo.Comments_Comment_SelectById",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    comment = (DataMapper<Comment>.Instance.MapToObject(reader));
                });
            return comment;
        }
        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.Comments_Comment_Delete",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@id", id);
                });
        }
    }
}
