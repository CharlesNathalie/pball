namespace pball.Services.Tests;

public partial class LeagueContactServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_Good_Test(string culture)
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
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_Authorization_Error_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

        if (LoggedInService != null)
        {
            LoggedInService.LoggedInContactInfo.LoggedInContact = null;
        }

        if (LeagueContactService != null)
        {
            var actionRes = await LeagueContactService.AddLeagueContactAsync(new LeagueContact());
            bool? boolRet = await DoBadRequestLeagueContactTestAsync(PBallRes.YouDoNotHaveAuthorization, actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_LeagueContactID_Error_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

        if (LeagueContactService != null)
        {
            LeagueContact leagueContact = await FillLeagueContact();
            Assert.True(leagueContact.LeagueID > 0);
            Assert.True(leagueContact.ContactID > 0);

            leagueContact.LeagueContactID = 1;

            var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
            bool? boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._ShouldBeEqualTo_, "LeagueContactID", "0"), actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_LeagueID_Error_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

        if (LeagueContactService != null)
        {
            LeagueContact leagueContact = await FillLeagueContact();
            Assert.True(leagueContact.LeagueID > 0);
            Assert.True(leagueContact.ContactID > 0);

            leagueContact.LeagueID = 0;

            var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
            bool? boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._IsRequired, "LeagueID"), actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);

            leagueContact.LeagueID = 10000;

            actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
            boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "League", "LeagueID", leagueContact.LeagueID.ToString()), actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_ContactID_Error_Test(string culture)
    {
        Assert.True(await _LeagueContactServiceSetupAsync(culture));

        if (LeagueContactService != null)
        {
            LeagueContact leagueContact = await FillLeagueContact();
            Assert.True(leagueContact.LeagueID > 0);
            Assert.True(leagueContact.ContactID > 0);

            leagueContact.ContactID = 0;

            var actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
            bool? boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._IsRequired, "ContactID"), actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);

            leagueContact.ContactID = 10000;

            actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
            boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes.CouldNotFind_With_Equal_, "Contact", "ContactID", leagueContact.ContactID.ToString()), actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task AddLeagueContactAsync_AlreadyExist_Error_Test(string culture)
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

            actionRes = await LeagueContactService.AddLeagueContactAsync(leagueContact);
            bool? boolRet = await DoBadRequestLeagueContactTestAsync(string.Format(PBallRes._AlreadyExist, "LeagueContact"), actionRes);
            Assert.NotNull(boolRet);
            Assert.True(boolRet);
        }
    }
}

