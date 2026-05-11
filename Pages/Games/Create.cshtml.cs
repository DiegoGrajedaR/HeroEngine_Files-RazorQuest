using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGameManager.Data;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class CreateModel : PageModel
    {
        //Cambiar GameService i fer ús de GameStoreContext de EF
        //private readonly GameService GameService;
        private readonly GameStoreContext _context;
        public SelectList DeveloperList { get; set; } = default!;

        // [BindProperty] connect the HTML fields to the variable
        [BindProperty]
        public Game Game { get; set; } = new Game();

        public CreateModel(GameStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            DeveloperList = new SelectList(await _context.Developers.ToListAsync(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Game.Developer");

            // We check if the data is valid.
            if (!ModelState.IsValid)
            {
                DeveloperList = new SelectList(await _context.Developers.ToListAsync(), "Id", "Name");
                return Page(); // If error, reload the page
            }

            _context.Games.Add(Game);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index"); // If well, return to the list
        }
    }
}
