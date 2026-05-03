using System.Xml.Linq;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameService
    {
        private readonly List<Game> _games;
        private int _nextId = 1;

        public GameService()
        {
            _games = new List<Game>();
        }

        public List<Game> GetAll() => _games;

        public Game? GetById(int id) => _games.FirstOrDefault(g => g.Id == id);

        public void Add(Game game)
        {
            game.Id = _nextId++;
            _games.Add(game);
        }

        public void Update(Game game)
        {
            var existingGame = GetById(game.Id);
            if (existingGame != null)
            {
                existingGame.Title = game.Title;
                existingGame.Genre = game.Genre;
                existingGame.Year = game.Year;
                existingGame.Score = game.Score;
                existingGame.Description = game.Description;
            }
        }

        public void Delete(int id)
        {
            var game = GetById(id);
            if (game != null)
            {
                _games.Remove(game);
            }
        }
    }
}
