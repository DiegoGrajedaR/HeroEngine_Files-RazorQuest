using System.Xml.Linq;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameService
    {
        private readonly List<Game> GamesList;
        private int NextId = 1;

        //Constructor
        public GameService()
        {
            GamesList = new List<Game>();
        }

        //Methods
        public List<Game> GetAll() => GamesList;

        public Game? GetById(int id) => GamesList.FirstOrDefault(g => g.Id == id);

        public void Add(Game game)
        {
            game.Id = NextId++;
            GamesList.Add(game);
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
                GamesList.Remove(game);
            }
        }
    }
}
