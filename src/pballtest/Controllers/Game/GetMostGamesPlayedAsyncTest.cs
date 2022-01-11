namespace pball.Controllers.Tests;

public partial class GameControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetMostGamesPlayedBetweenDatesAsync_Good_Test(string culture)
    {
        Assert.True(await GameControllerSetup(culture));

        if (db != null)
        {
            Contact? contact = (from c in db.Contacts
                                select c).FirstOrDefault();

            if (contact != null)
            {
                LeagueGamesModel leagueGamesModel = new LeagueGamesModel()
                {
                    LeagueID = 0,
                    StartDate = DateTime.Now.AddDays(-20),
                    EndDate = DateTime.Now.AddDays(0),
                };

                List<MostGamesPlayedModel>? mostGamesPlayedModelList = await DoOkMostGamesPlayedBetweenDates(leagueGamesModel, contact, culture);
                Assert.NotNull(mostGamesPlayedModelList);
                if (mostGamesPlayedModelList != null)
                {
                    Assert.NotEmpty(mostGamesPlayedModelList);
                }
            }
        }
    }
}

