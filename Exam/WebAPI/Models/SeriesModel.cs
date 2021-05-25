
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class SeriesModel
    {
        public string Title { get; set; }
        public string ReleaseYear { get; set; }
        public string Description { get; set; }
        public string genre { get; set; }
        public List<string> actors {get; set;}
        public List<string> directors {get; set;}
        public List<string> writers {get; set;}
        public int seasons {get; set;}
    }
}