namespace pball.Controllers.Tests;

public partial class BaseControllerTests
{
    protected async Task<LeagueContact> FillLeagueContactAsync(int LeagueID, int ContactID, bool IsLeagueAdmin)
    {

        LeagueContact leagueContact = new LeagueContact()
        {
            LeagueID = LeagueID,
            ContactID = ContactID,
            Removed = false,
            IsLeagueAdmin = IsLeagueAdmin
        };

        return await Task.FromResult(leagueContact);
    }
}

