using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameManager.Data;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class DeleteModel : PageModel
    {
        //private readonly GameService _gameService;
        private readonly GameStoreContext _context;

        public DeleteModel(GameStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Game Game { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games
                .Include(g => g.Developer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null) return NotFound();

            Game = game;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games.FindAsync(id);

            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
