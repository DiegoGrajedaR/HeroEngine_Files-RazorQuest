using System.Xml.Linq;
using VideoGameManager.Models;

namespace VideoGameManager.Services
{
    public class GamesRanking
    {
        private readonly string _xmlPath = "wwwroot/data/games_ranking.xml";

        public void GenerateRankingXml(IEnumerable<Game> games)
        {
            Directory.CreateDirectory("wwwroot/data");

            // Sort by descending score using LINQ
            var rankedGames = games.OrderByDescending(g => g.Score).ToList();

            // Create the required XML structure
            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("AppConfig",
                    new XElement("AppTitle", "VideoGame Ranking"),
                    new XElement("Games",
                        from game in rankedGames
                        select new XElement("Game",
                            new XElement("id", game.Id),
                            new XElement("score", game.Score),
                            new XElement("title", game.Title),
                            new XElement("genre", game.Genre),
                            new XElement("year", game.Year),
                            new XElement("description", game.Description)
                        )
                    )
                )
            );

            // Save file
            doc.Save(_xmlPath);
        }
    }
}
