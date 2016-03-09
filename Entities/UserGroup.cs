using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserGroup
    {
        public int UserGroupId { get; set; }
        public string UserId { get; set; }
        public string GroupName { get; set; }
        public DateTime DateCreated { get; set; }
        [PetaPoco.Ignore] 
        public System.Collections.Generic.List<AspNetUsers> Users { get; set; }
    }
}
