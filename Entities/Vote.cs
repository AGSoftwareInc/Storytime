using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Vote
    {
        public int VoteId { get; set; }
        public string UserId { get; set; }
        public int StorytimePostId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool UserNotified { get; set; }
    }
}
