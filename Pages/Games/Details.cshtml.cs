using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class DetailsModel : PageModel
    {
        private readonly GameService GameService;
        public Game? Game { get; set; }

        public DetailsModel(GameService gameService)
        {
            GameService = gameService;
        }
        
        public IActionResult OnGet(int id)
        {
            // Search game by id
            Game = GameService.GetById(id);

            // If the game does not exist, return (Not Found) error.
            if (Game == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
