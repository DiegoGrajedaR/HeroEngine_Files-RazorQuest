using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameManager.Data;
using VideoGameManager.Models;
using VideoGameManager.Services;

namespace VideoGameManager.Pages.Games
{
    public class IndexModel : PageModel
    {
        //private readonly GameService _gameService;
        private readonly GameStoreContext _context;
        public List<Game> GamesList { get; set; } = new List<Game>();

        public IndexModel(GameStoreContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            //Here you get the list of games by calling the GameService
            //GamesList = _gameService.GetAll();

            //Si taula Developers esta buida afegim el seguent elements
            if (!_context.Developers.Any())
            {
                var dev1 = new Developer { Name = "Nintendo", Country = "Japan", FoundedYear = 1889 };
                var dev2 = new Developer { Name = "CD Projekt Red", Country = "Poland", FoundedYear = 1994 };
                _context.Developers.AddRange(dev1, dev2);
                _context.SaveChanges();

                _context.Games.AddRange(
                    new Game
                    {
                        Title = "The Legend of Zelda: TotK",
                        Genre = "Adventure",
                        Year = 2023,
                        Score = 9.8,
                        DeveloperId = dev1.Id
                    },
                    new Game
                    {
                        Title = "Mario Kart 8",
                        Genre = "Racing",
                        Year = 2014,
                        Score = 8.7,
                        DeveloperId = dev1.Id
                    },
                    new Game
                    {
                        Title = "The Witcher 3",
                        Genre = "RPG",
                        Year = 2015,
                        Score = 9.5,
                        DeveloperId = dev2.Id
                    }
                );
                _context.SaveChanges();
            }

            GamesList = await _context.Games.Include(g => g.Developer).ToListAsync();
        }
    }
}
