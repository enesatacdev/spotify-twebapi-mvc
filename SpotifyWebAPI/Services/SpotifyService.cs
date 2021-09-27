using SpotifyWebAPI.Helpers;
using SpotifyWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpotifyWebAPI.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly HttpClient _httpClient;
        public SpotifyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Release>> GetNewReleases(string code, int limit, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"browse/new-releases?country={code}&limit={limit}");
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<GetNewReleaseResult>(responseStream);

            return responseObject?.albums?.items.Select(x => new Release
            {
                Name = x.name,
                Date = x.release_date,
                ImageUrl = x.images.FirstOrDefault().url,
                Link = x.external_urls.spotify,
                Artists = String.Join(",",x.artists.Select(a=> a.name))

            });
        }

        public async Task<IEnumerable<Search>> Search(string query, string type, string market, int limit,string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"search?q={query}&type={type}&market={market}&limit={limit}");
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<SearchResult.Root>(responseStream);


            return responseObject?.tracks?.items?.Select(x=> new Search
            {
                Name = x.album.name,
                Image = x.album.images.Select(y=> y.url).FirstOrDefault(),
                Artists = String.Join(",", x.album.artists.Select(y=> y.name)),
                Link = x.album.external_urls.spotify,
                ReleaseDate = x.album.release_date
            });
        }
    }
}
