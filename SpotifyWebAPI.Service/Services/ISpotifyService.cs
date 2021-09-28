using SpotifyWebAPI.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyWebAPI.Service.Services
{
    public interface ISpotifyService
    {
        Task<IEnumerable<Release>> GetNewReleases(string code, int limit, string token);

        Task<IEnumerable<Search>> Search(string query, string market, int limit, string token);

        Task<IEnumerable<SearchArtist>> SearchArtist(string query, string market, int limit, string token);
    }
}
