using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Models.Domain.Comments
{
    public class SpecificComment
    {
        public int Id { get; set; }
        public int TableKeyId { get; set; }
        public string TableName { get; set; }
        public int UserBaseId { get; set; }
        public int PageNumber { get; set; }
        public string SortBy { get; set; }
        public string CommentText { get; set; }
        public int ParentCommentId { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime? ModifiedDate { get; set; }
        public int CreatedById { get; set; } 
        public string AvatarUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public int IsAuthor { get; set; }
        public int TotalComments { get; set; }
        public int TotalPages { get; set; }
        public bool CommentEdited { get; set; }
        public List<SpecificComment> Replies { get; set; }
    }
}

