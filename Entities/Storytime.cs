using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    class Storytime
    {
        public int StorytimeId { get; set; }
        public string StorytimeTitle { get; set; }

        public List<StorytimePost> StorytimePosts { get; set; }

    }
}
