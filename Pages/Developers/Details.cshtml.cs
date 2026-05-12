using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Data;
using VideoGameManager.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoGameManager.Pages.Developers
{
    public class DetailsModel : PageModel
    {
        private readonly GameStoreContext _context;

        public DetailsModel(GameStoreContext context) => _context = context;

        public Developer Developer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var developer = await _context.Developers
                .Include(d => d.Games)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (developer == null) return NotFound();

            Developer = developer;
            return Page();
        }
    }
}
