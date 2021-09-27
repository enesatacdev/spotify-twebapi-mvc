using SpotifyWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyWebAPI.Services
{
    public interface ISpotifyService
    {
        Task<IEnumerable<Release>> GetNewReleases(string code, int limit, string token);

        Task<IEnumerable<Search>> Search(string query, string type, string market, int limit, string token);
    }
}
