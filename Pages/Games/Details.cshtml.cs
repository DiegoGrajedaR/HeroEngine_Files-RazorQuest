using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameManager.Data;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class DetailsModel : PageModel
    {
        //private readonly GameService GameService;
        private readonly GameStoreContext _context;
        public Game? Game { get; set; }

        public DetailsModel(GameStoreContext context)
        {
            _context = context;
        }
        
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
    }
}
