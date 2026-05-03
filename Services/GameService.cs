using System.Xml.Linq;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameService
    {
        private readonly List<Game> GamesList;
        private int NextId = 1;

        //Route of log.txt file
        private readonly string _logPath = "wwwroot/data/activity_log.txt";

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
            LogAction("CREATE", game.Title);
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
                LogAction("UPDATE", game.Title);
            }
        }

        public void Delete(int id)
        {
            var game = GetById(id);
            if (game != null)
            {
                GamesList.Remove(game);
                LogAction("DELETE", game.Title);
            }
        }

        //New method to record the activity in our game library
        private void LogAction(string action, string gameTitle)
        {
            string logLine = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] [{action}] [{gameTitle}]\n";
            File.AppendAllText(_logPath, logLine);
        }

        //Method to import games from JSON 
        public void SetAll(List<Game> importedGames)
        {
            GamesList.Clear();
            GamesList.AddRange(importedGames);
            NextId = GamesList.Any() ? GamesList.Max(g => g.Id) + 1 : 1;
        }
    }
}
