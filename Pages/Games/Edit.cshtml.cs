using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VideoGameManager.Data;
using VideoGameManager.Models;
using VideoGameManager.Services;
using Microsoft.EntityFrameworkCore;

namespace VideoGameManager.Pages.Games
{
    public class EditModel : PageModel
    {
        //private readonly GameService GameService;
        private readonly GameStoreContext _context;

        public SelectList DeveloperList { get; set; } = default!;

        // [BindProperty] connect the HTML fields to the variable
        [BindProperty]
        public Game Game { get; set; } = default!;

        public EditModel(GameStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Game = await _context.Games.Include(g => g.Developer).FirstOrDefaultAsync(m => m.Id == id);

            if (Game == null) return NotFound();

            DeveloperList = new SelectList(await _context.Developers.ToListAsync(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //Eliminar validation de Developer
            ModelState.Remove("Game.Developer");

            if (!ModelState.IsValid)
            {
                // En cas de error,tornar a carregar la llista abans de retornar a la llista
                DeveloperList = new SelectList(await _context.Developers.ToListAsync(), "Id", "Name");
                return Page();
            }

            _context.Update(Game);

            try
            {
                await _context.SaveChangesAsync();
            }catch (DbUpdateConcurrencyException) {
                if (!_context.Games.Any(e => e.Id == Game.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return RedirectToPage("./Index");
        }
    }
}
