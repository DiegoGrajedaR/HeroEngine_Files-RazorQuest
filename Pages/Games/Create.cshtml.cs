using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class CreateModel : PageModel
    {
        private readonly GameService GameService;

        public CreateModel(GameService gameService)
        {
            GameService = gameService;
        }

        // [BindProperty] connect the HTML fields to the variable
        [BindProperty]
        public Game Game { get; set; } = new Game();

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            // We check if the data is valid.

            if (!ModelState.IsValid)
            {
                return Page(); // If error, reload the page
            }

            GameService.Add(Game);
            return RedirectToPage("./Index"); // If well, return to the list
        }
    }
}
