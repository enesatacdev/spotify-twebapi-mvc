using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyWebAPI.Models
{
    public class Search
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }

        public string Artists { get; set; }
        public string ReleaseDate { get; set; }
    }
}
