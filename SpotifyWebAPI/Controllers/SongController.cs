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
    public class SongController : Controller
    {
        private readonly ISpotifyService _spotifyService;
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IConfiguration _configuration;
        public SongController(ISpotifyService spotifyService, ISpotifyAccountService spotifyAccountService, IConfiguration configuration)
        {
            _spotifyService = spotifyService;
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewModel vm = new ViewModel();
            vm.Search = null;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(string txtSong)
        {
            ViewModel vm = new ViewModel();
            vm.Search = GetSearch(txtSong);

            return View(vm);
        }
        private async Task<IEnumerable<Search>> GetSearch(string query)
        {
            try
            {
                var token = await _spotifyAccountService.GetToken(_configuration["Spotify:ClientId"], _configuration["Spotify:ClientSecret"]);

                var search = await _spotifyService.Search(query, "us", 10, token);

                return search;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                return Enumerable.Empty<Search>();
            }
        }
    }
}
