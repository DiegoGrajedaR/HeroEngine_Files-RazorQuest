using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Files
{
    public class IndexModel : PageModel
    {
        private readonly GameService GameService;
        private readonly GameRepository GameRepository;
        private readonly GamesExporter GamesExporter;
        private readonly GamesRanking GamesRanking;
        public string[] LogEntries { get; set; } = Array.Empty<string>();
        public bool XmlGenerated { get; set; } = false;

        public IndexModel(GameService gameService, GameRepository gameRepository, GamesExporter gamesExporter, GamesRanking gamesRanking)
        {
            GameService = gameService;
            GameRepository = gameRepository;
            GamesExporter = gamesExporter;
            GamesRanking = gamesRanking;
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

        // Execute when click "Export CSV"
        public IActionResult OnPostExportCsv()
        {
            var games = GameService.GetAll();
            byte[] fileBytes = GamesExporter.ExportToCsv(games);
            // Return the file for the browser to download
            return File(fileBytes, "text/csv", "games.csv");
        }

        // Execute when click "Generate XML"
        public IActionResult OnPostGenerateXml()
        {
            var games = GameService.GetAll();
            GamesRanking.GenerateRankingXml(games);

            TempData["XmlSuccess"] = "XML Ranking Games generated successfully!";
            return RedirectToPage();
        }
    }
}
