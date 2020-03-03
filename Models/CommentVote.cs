using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Models.Domain.Comments
{
    public class CommentVote
    {
        public int CommentId { get; set; }

        public int Upvote { get; set; }

        public int  Downvote{ get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedById { get; set; }


    }
}
