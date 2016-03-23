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
        public DateTime DateCreated { get; set; }
    }
}
