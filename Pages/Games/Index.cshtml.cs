using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly GameService _gameService;
        public List<Game> GamesList { get; set; } = new List<Game>();

        public IndexModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public void OnGet()
        {
            //Here you get the list of games by calling the GameService
            GamesList = _gameService.GetAll();
        }
    }
}
