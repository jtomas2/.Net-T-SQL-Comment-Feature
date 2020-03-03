using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospect.Models.Domain.Comments
{
    public class Comment
    {

        public int Id { get; set; }

        public string CommentText { get; set; }

        public int TableKeyId { get; set; }

        public string TableName { get; set; }

        public Boolean CommentEdited { get; set; } 

        public int CreatedById { get; set; }

       

        public int ModifiedById { get; set; }

    }
}
