using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameManager.Data;
using VideoGameManager.Models;

namespace VideoGameManager.Pages.Developers
{
    public class DeleteModel : PageModel
    {
        private readonly GameStoreContext _context;

        [BindProperty]
        public Developer Developer { get; set; } = default!;


        public DeleteModel(GameStoreContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null) return NotFound();

            var developer = await _context.Developers
                .Include(d => d.Games)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (developer == null) return NotFound();

            Developer = developer;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {

            var developer = await _context.Developers
                .Include(d => d.Games)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (developer == null)
            {
                return NotFound();
            }

            if (developer.Games != null && developer.Games.Any())
            {
                ModelState.AddModelError("", "Error: Aquest developer té jocs. Elimina primer els jocs.");
                Developer = developer;

                return Page();
            }

            _context.Developers.Remove(developer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
