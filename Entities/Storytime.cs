using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Storytime
    {
        public int StorytimeId { get; set; }
        public string StorytimeTitle { get; set; }
        public DateTime DateCreated { get; set; }
        public int StorytimeTypeId
        {
            get { return (int)this.StorytimeType; }
        }
        public string UserId { get; set; }

        [PetaPoco.Ignore]
        public List<AspNetUsers> Users { get; set; }

        [PetaPoco.Ignore]
        public List<StorytimePost> StorytimePosts { get; set; }

        [PetaPoco.Ignore]
        public int StorytimeGroupId { get; set; }

        [PetaPoco.Ignore]
        public StorytimeType StorytimeType { get; set; }

    }
}
