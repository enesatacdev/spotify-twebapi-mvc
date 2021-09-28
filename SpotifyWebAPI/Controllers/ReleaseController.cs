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
    public class ReleaseController : Controller
    {
        private readonly ISpotifyService _spotifyService;
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IConfiguration _configuration;
        public ReleaseController(ISpotifyService spotifyService, ISpotifyAccountService spotifyAccountService, IConfiguration configuration)
        {
            _spotifyService = spotifyService;
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
        }
        
        public IActionResult Index()
        {
            ViewModel vm = new ViewModel();
            vm.GetReleases = GetReleases();

            return View(vm);
        }
        private async Task<IEnumerable<Release>> GetReleases()
        {

            try
            {
                var token = await _spotifyAccountService.GetToken(_configuration["Spotify:ClientId"], _configuration["Spotify:ClientSecret"]);

                var newReleases = await _spotifyService.GetNewReleases("TR", 18, token);


                return newReleases;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);

                return Enumerable.Empty<Release>();
            }
        }
    }
}
