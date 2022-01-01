namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<Game> FillGame(int Team1Player1, int Team1Player2, int Team2Player1, int Team2Player2, int LeagueID)
    {
        Random random = new Random();

        Game game = new Game()
        {
            Team1Player1 = Team1Player1,
            Team1Player2 = Team1Player2,
            Team2Player1 = Team2Player1,
            Team2Player2 = Team2Player2,
            Team1Scores = random.Next(1, 9),
            Team2Scores = 11,
            GameDate = DateTime.Now.AddDays(random.Next(1, 30) * -1),
            LeagueID = LeagueID,
            Removed = false,
        };
        
        return await Task.FromResult(game);
    }
}

