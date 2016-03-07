using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StorytimeGroup
    {
        public int StorytimeGroupId { get; set; }
        public string GroupName { get; set; }
        public int StorytimeId { get; set; }
        public int UserGroupId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
