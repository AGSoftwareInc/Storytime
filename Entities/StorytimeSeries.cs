using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StorytimeSeries
    {
        public int StorytimeId { get; set; }
        public int StorytimeSeriesId { get; set; }
        public string UserId { get; set; }
        public string SeriesText { get; set; }
        public DateTime DateCreated { get; set; }
        public bool UsersNotified { get; set; }
        [PetaPoco.Ignore]
        public bool ImagePostingExpired
        {
            get
            {
                if (this.DateCreated.AddHours(12) > System.DateTime.Now)
                    return false;
                else
                    return true;
            }
        }
        [PetaPoco.Ignore]
        public bool VotingExpired { 
            get
            {
                if (this.DateCreated.AddHours(24) > System.DateTime.Now)
                    return false;
                else
                    return true;
            }
        }
    }
}
