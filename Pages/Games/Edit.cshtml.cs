using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class EditModel : PageModel
    {
        private readonly GameService GameService;

        public EditModel(GameService gameService)
        {
            GameService = gameService;
        }

        // [BindProperty] connect the HTML fields to the variable
        [BindProperty]
        public Game Game { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            var game = GameService.GetById(id);
            if (game == null)
            {
                return NotFound();
            }

            Game = game; // Load the existing game
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            GameService.Update(Game);
            return RedirectToPage("./Index");
        }
    }
}
