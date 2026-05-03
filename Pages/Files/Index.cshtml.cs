using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Files
{
    public class IndexModel : PageModel
    {
        private readonly GameService GameService;
        private readonly GameRepository GameRepository;
        public string[] LogEntries { get; set; } = Array.Empty<string>();

        public IndexModel(GameService gameService, GameRepository gameRepository)
        {
            GameService = gameService;
            GameRepository = gameRepository;
        }

        public void OnGet()
        {
            string logPath = "wwwroot/data/activity_log.txt";
            if (System.IO.File.Exists(logPath))
            {
                // Read all the lines from text file
                LogEntries = System.IO.File.ReadAllLines(logPath);
            }
        }

        // Execute when click "Export JSON"
        public IActionResult OnPostExportJson()
        {
            var allGames = GameService.GetAll();
            GameRepository.SaveAll(allGames);
            return RedirectToPage();
        }

        // Execute when click "Import JSON"
        public IActionResult OnPostImportJson()
        {
            var importedGames = GameRepository.LoadAll();
            GameService.SetAll(importedGames); // Overwrite memory
            return RedirectToPage();
        }
    }
}
