using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SpotifyWebAPI.Service.Models;
using SpotifyWebAPI.Service.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyWebAPI.Controllers
{
    public class ArtistController : Controller
    {
        private readonly ISpotifyService _spotifyService;
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IConfiguration _configuration;
        public ArtistController(ISpotifyService spotifyService, ISpotifyAccountService spotifyAccountService, IConfiguration configuration)
        {
            _spotifyService = spotifyService;
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewModel vm = new ViewModel();
            vm.SearchArtist = null;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(string txtArtist)
        {
            ViewModel vm = new ViewModel();
            vm.SearchArtist = GetSearchArtist(txtArtist, "us");

            return View(vm);
        }
        private async Task<IEnumerable<SearchArtist>> GetSearchArtist(string query, string market)
        {
            try
            {
                var token = await _spotifyAccountService.GetToken(_configuration["Spotify:ClientId"], _configuration["Spotify:ClientSecret"]);

                var search = await _spotifyService.SearchArtist(query, market, 10, token);

                return search;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                return Enumerable.Empty<SearchArtist>();
            }
        }
    }
}
