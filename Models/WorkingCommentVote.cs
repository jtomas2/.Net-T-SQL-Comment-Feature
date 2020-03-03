using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Models.Domain.Comments
{
    public class WorkingCommentVote
    {
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public int UserBaseId { get; set; }
        public int CommentId { get; set; }
        public string Action { get; set; }
    }
}
