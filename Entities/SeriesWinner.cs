using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SeriesWinner
    {
        public int StorytimeSeriesId { get; set; }
        public int StorytimeTypeId { get; set; }
        public int StorytimeId { get; set; }
        public string SeriesText { get; set; }
        public string Username { get; set; }
        public int Votes { get; set; }
    }
}
