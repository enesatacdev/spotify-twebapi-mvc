using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyWebAPI.Service.Models
{
    public class SearchArtist
    {
        public string Name { get; set; }
        public string Genres { get; set; }
        public int Followers { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }

    }
}
