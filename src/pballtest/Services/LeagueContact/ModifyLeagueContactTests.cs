namespace pball.Services.Tests;

public partial class LeagueContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueContactAsync_Good_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

        if (LeagueContactService != null)
        {
            LeagueContact leagueContact = await FillLeagueContact();
            Assert.True(leagueContact.ContactID > 0);

            var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
            LeagueContact? leagueContactRet = await DoOKTestReturnLeagueContactAsync(actionRes);
            Assert.NotNull(leagueContactRet);

            if (leagueContactRet != null)
            {
                leagueContactRet.IsLeagueAdmin = !leagueContact.IsLeagueAdmin;

                var actionModifyRes = await LeagueContactService.ModifyLeagueContactAsync(leagueContactRet);
                LeagueContact? leagueContactRet2 = await DoOKTestReturnLeagueContactAsync(actionModifyRes);
                Assert.NotNull(leagueContactRet2);
                if (leagueContactRet2 != null)
                {
                    Assert.Equal(leagueContactRet.LeagueID, leagueContactRet2.LeagueID);
                    Assert.Equal(leagueContactRet.IsLeagueAdmin, leagueContactRet2.IsLeagueAdmin);
                }
            }
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueContactAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

        if (LoggedInService != null)
        {
            LoggedInService.LoggedInContactInfo.LoggedInContact = null;
        }

        if (LeagueContactService != null)
        {
            var actionModifyRes = await LeagueContactService.ModifyLeagueContactAsync(new LeagueContact());
            bool? boolRet = await DoBadRequestLeagueContactTestAsync(PBallRes.YouDoNotHaveAuthorization, actionModifyRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ModifyLeagueContactAsync_LeagueContactID_Error_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

        if (LeagueContactService != null)
        {
            LeagueContact leagueContact = await FillLeagueContact();
            Assert.True(leagueContact.ContactID > 0);

            var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
            LeagueContact? leagueContactRet = await DoOKTestReturnLeagueContactAsync(actionRes);
            Assert.NotNull(leagueContactRet);

            if (leagueContactRet != null)
            {
                leagueContactRet.LeagueContactID = 0;

                var actionModifyRes = await LeagueContactService.ModifyLeagueContactAsync(leagueContactRet);
                bool? boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._IsRequired, "LeagueContactID"), actionModifyRes);
                Assert.NotNull(boolRet);
                Assert.True(boolRet);
            }
        }
    }
}

