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
        public int StorytimeId { get; set; }
        public int SeriesId { get; set; }
        public string UserId { get; set; }
        public string PostText { get; set; }
        public string ImagePath { get; set; }
        public int Votes { get; set; }
        [PetaPoco.Ignore]
        public bool Voted { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
