namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<League> FillLeagueAsync()
    {
        Random random = new Random();

        League league = new League()
        {            
            LeagueName = $"League Name { random.Next(1, 10000) }",
            PercentPointsFactor = random.NextDouble() * 10,
            PlayerLevelFactor = random.NextDouble() * 10,
            PointsToLoosers = random.NextDouble() * 10,
            PointsToWinners = random.NextDouble() * 10,
            Removed = false,
        };

        return await Task.FromResult(league);
    }
}

