using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Data;
using VideoGameManager.Models;

namespace VideoGameManager.Pages.Developers
{
    public class CreateModel : PageModel
    {
        private readonly GameStoreContext _context;

        public CreateModel(GameStoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Developer Developer { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Developers.Add(Developer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
