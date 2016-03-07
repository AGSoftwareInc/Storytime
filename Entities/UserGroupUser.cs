using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserGroupUser
    {
        public int UserGroupUserId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

    }
}
