using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class DeleteModel : PageModel
    {
        private readonly GameService _gameService;

        public DeleteModel(GameService gameService)
        {
            _gameService = gameService;
        }

        [BindProperty]
        public Game Game { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            // Load the game to show it to the user for review.
            var game = _gameService.GetById(id);
            if (game == null)
            {
                return NotFound();
            }

            Game = game;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            // When clicks the "Delete" button, we delete the game and redirect
            _gameService.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}
