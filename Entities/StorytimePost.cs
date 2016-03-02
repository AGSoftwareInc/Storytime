using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StorytimePost
    {
        public int StorytimePostId { get; set; }
        public int UserId { get; set; }
        public string PostText { get; set; }
        public string ImagePath { get; set; }
        public int SentToUserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
