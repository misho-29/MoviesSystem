using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Models.Responses
{
    public class MovieDetailsResponse
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string ImDbRating { get; set; }
        public Wikipedia Wikipedia { get; set; }
    }

    public class Wikipedia
    {
        public PlotShort PlotShort { get; set; }
    }

    public class PlotShort
    {
        public string Html { get; set; }
    }
}
