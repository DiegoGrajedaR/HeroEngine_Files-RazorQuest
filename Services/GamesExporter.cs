using System.Text;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GamesExporter
    {
        public byte[] ExportToCsv(IEnumerable<Game> games)
        {
            var builder = new StringBuilder();

            //  Write the CSV header
            builder.AppendLine(string.Join(',', new[] { "Id", "Title", "Genre", "Year", "Score" }));

            // Write every game
            foreach (var game in games)
            {
                string title = game.Title.Contains(",") ? $"\"{game.Title}\"" : game.Title;
                string genre = game.Genre.Contains(",") ? $"\"{game.Genre}\"" : game.Genre;

                builder.AppendLine($"{game.Id},{title},{genre},{game.Year},{game.Score}");
            }

            // Return the file as a byte array so we can download it from the browser.
            return Encoding.UTF8.GetBytes(builder.ToString());
        }
    }
}
