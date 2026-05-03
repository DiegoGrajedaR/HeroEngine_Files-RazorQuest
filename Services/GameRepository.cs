using System;
using System.Text.Json;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GameRepository
    {
        //Route of JSON file
        private readonly string _filePathJSON = "wwwroot/data/games.json";

        public void SaveAll(IEnumerable<Game> games)
        {
            Directory.CreateDirectory("wwwroot/data");
            // Serialize objects 
            string json = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePathJSON, json);
        }

        public List<Game> LoadAll()
        {
            // Check if the file exists before reading
            if (!File.Exists(_filePathJSON))
            {
                return new List<Game>();
            }

            string json = File.ReadAllText(_filePathJSON);
            return JsonSerializer.Deserialize<List<Game>>(json) ?? new List<Game>();
        }
    }
}
