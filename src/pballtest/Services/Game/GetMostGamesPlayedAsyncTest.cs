namespace pball.Services.Tests;

public partial class GameServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task GetMostGamesPlayedBetweenDatesAsync_Good_Test(string culture)
    {
        Assert.True(await _GameServiceSetupAsync(culture));

        Assert.NotNull(db);
        if (db != null)
        {
            Assert.NotNull(db.LeagueContacts);
            if (db.LeagueContacts != null)
            {
                LeagueContact? leagueContact = (from c in db.LeagueContacts
                                                select c).FirstOrDefault();

                Assert.NotNull(leagueContact);

                if (leagueContact != null)
                {

                    LeagueGamesModel leagueGamesModel = new LeagueGamesModel()
                    {
                        LeagueID = leagueContact.LeagueID,
                        StartDate = DateTime.Now.AddDays(-20),
                        EndDate = DateTime.Now.AddDays(0),
                    };

                    if (GameService != null)
                    {
                        var actionRes = await GameService.GetMostGamesPlayedBetweenDatesAsync(leagueGamesModel);
                        List<MostGamesPlayedModel>? mostGamePlayedModelList = await DoOKTestReturnMostGamePlayedModelListAsync(actionRes);
                        Assert.NotNull(mostGamePlayedModelList);
                        Assert.NotEmpty(mostGamePlayedModelList);
                    }
                }
            }
        }
    }
}

