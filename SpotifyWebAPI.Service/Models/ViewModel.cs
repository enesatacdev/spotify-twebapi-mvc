using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyWebAPI.Service.Models
{
    public class ViewModel
    {
        public Task<IEnumerable<Release>> GetReleases { get; set; }
        public Task<IEnumerable<Search>> Search { get; set; }
        public Task<IEnumerable<SearchArtist>> SearchArtist { get; set; }
    }
}
