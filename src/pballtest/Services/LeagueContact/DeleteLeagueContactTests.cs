namespace pball.Services.Tests;

public partial class LeagueContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueContactAsync_Good_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

        if (LeagueContactService != null)
        {
            LeagueContact leagueContact = await FillLeagueContact();
            Assert.True(leagueContact.LeagueID > 0);
            Assert.True(leagueContact.ContactID > 0);

            var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
            LeagueContact? leagueContactRet = await DoOKTestReturnLeagueContactAsync(actionRes);
            if (leagueContactRet != null)
            {
                Assert.True(leagueContactRet.LeagueContactID > 0);
            }

            if (leagueContactRet != null)
            {
                var actionDeleteRes = await LeagueContactService.DeleteLeagueContactAsync(leagueContactRet.LeagueContactID);
                LeagueContact? leagueContactRet2 = await DoOKTestReturnLeagueContactAsync(actionDeleteRes);
                if (leagueContactRet2 != null)
                {
                    Assert.True(leagueContactRet2.LeagueContactID > 0);
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueContactAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

        if (LoggedInService != null)
        {
            LoggedInService.LoggedInContactInfo.LoggedInContact = null;
        }

        if (LeagueContactService != null)
        {
            var actionDeleteRes = await LeagueContactService.DeleteLeagueContactAsync(1);
            bool? boolRet = await DoBadRequestLeagueContactTestAsync(PBallRes.YouDoNotHaveAuthorization, actionDeleteRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task DeleteLeagueContactAsync_LeagueContactID_Error_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

        if (LeagueContactService != null)
        {
            LeagueContact leagueContact = await FillLeagueContact();
            Assert.True(leagueContact.LeagueID > 0);
            Assert.True(leagueContact.ContactID > 0);

            var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
            LeagueContact? leagueContactRet = await DoOKTestReturnLeagueContactAsync(actionRes);
            if (leagueContactRet != null)
            {
                Assert.True(leagueContactRet.LeagueContactID > 0);
            }

            if (leagueContactRet != null)
            {
                leagueContactRet.LeagueContactID = 0;

                var actionDeleteRes = await LeagueContactService.DeleteLeagueContactAsync(leagueContactRet.LeagueContactID);
                bool? boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._IsRequired, "LeagueContactID"), actionDeleteRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);

                leagueContactRet.LeagueContactID = 10000;

                actionDeleteRes = await LeagueContactService.DeleteLeagueContactAsync(leagueContactRet.LeagueContactID);
                boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "LeagueContact", "LeagueContactID", leagueContactRet.LeagueContactID.ToString()), actionDeleteRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
}

