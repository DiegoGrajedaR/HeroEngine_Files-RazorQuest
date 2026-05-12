using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VideoGameManager.Data;
using VideoGameManager.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoGameManager.Pages.Stats
{
    public class IndexModel : PageModel
    {
        private readonly GameStoreContext _context;

        public IndexModel(GameStoreContext context)
        {
            _context = context;
        }

        // Classes auxiliars
        public class DecadeStat
        {
            public int Decade { get; set; }
            public int Count { get; set; }
        }

        public class DeveloperStat
        {
            public string Name { get; set; } = string.Empty;
            public int GameCount { get; set; }
            public double AvgScore { get; set; }
        }

        // Variables per guardar els resultats de les consultes LINQ  
        public IList<Game> FilteredGames { get; set; } = new List<Game>();
        public IList<Game> Top5Games { get; set; } = new List<Game>();
        public IList<DecadeStat> GamesByDecade { get; set; } = new List<DecadeStat>();
        public IList<DeveloperStat> AvgByDev { get; set; } = new List<DeveloperStat>();
        public IList<Game> SearchResults { get; set; } = new List<Game>();
        public IList<Developer> ProductiveDevs { get; set; } = new List<Developer>();

        // --- Paràmetres d'entrada pels formularis (Mètode GET)
        public SelectList GenresList { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SelectedGenre { get; set; } // Per 6.1

        [BindProperty(SupportsGet = true)]
        public string? TitleFilter { get; set; } // Per 7.2

        [BindProperty(SupportsGet = true)]
        public string? GenreFilter { get; set; } // Per 7.2

        [BindProperty(SupportsGet = true)]
        public int? MinYear { get; set; } // Per 7.2

        [BindProperty(SupportsGet = true)]
        public int Threshold { get; set; } = 2; // Per 7.3


        public async Task OnGetAsync()
        {
            var genres = await _context.Games.Select(g => g.Genre).Distinct().ToListAsync();
            GenresList = new SelectList(genres);

            // TASCA 6. CONSULTES BÀSIQUES

            // 6.1 Filtratge i ordenació
            if (!string.IsNullOrEmpty(SelectedGenre))
            {
                FilteredGames = await _context.Games
                    .Where(g => g.Genre == SelectedGenre)
                    .OrderByDescending(g => g.Score)
                    .ToListAsync();
            }

            // 6.2 Top 5 de jocs millor valorats
            Top5Games = await _context.Games
                .Include(g => g.Developer)
                .OrderByDescending(g => g.Score)
                .Take(5)
                .ToListAsync();

            // 6.3 Jocs per dècada
            GamesByDecade = await _context.Games
                .GroupBy(g => (g.Year / 10) * 10)
                .Select(grp => new DecadeStat { Decade = grp.Key, Count = grp.Count() })
                .OrderBy(x => x.Decade)
                .ToListAsync();

            // TASCA 7. CONSULTES AVANÇADES

            // 7.1 Puntuació mitjana per developer
            AvgByDev = await _context.Developers
                .Include(d => d.Games)
                .Where(d => d.Games.Any())
                .Select(d => new DeveloperStat
                {
                    Name = d.Name,
                    GameCount = d.Games.Count,
                    AvgScore = d.Games.Average(g => g.Score)
                })
                .OrderByDescending(x => x.AvgScore)
                .ToListAsync();

            // 7.2 Recerca combinada
            var query = _context.Games.Include(g => g.Developer).AsQueryable();

            if (!string.IsNullOrEmpty(TitleFilter))
                query = query.Where(g => g.Title.Contains(TitleFilter));

            if (!string.IsNullOrEmpty(GenreFilter))
                query = query.Where(g => g.Genre == GenreFilter);

            if (MinYear.HasValue)
                query = query.Where(g => g.Year >= MinYear.Value);

            SearchResults = await query.OrderBy(g => g.Title).ToListAsync();


            // 7.3 Developers amb més de N jocs
            ProductiveDevs = await _context.Developers
                .Include(d => d.Games)
                .Where(d => d.Games.Count > Threshold)
                .OrderByDescending(d => d.Games.Count)
                .ToListAsync();
        }
    }
}
