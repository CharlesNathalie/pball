namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<LeagueContact> FillLeagueContact()
    {
        int LeagueID = 0;
        int ContactID = 0;

        if (db != null)
        {
            League? league = (from c in db.Leagues
                              select c).FirstOrDefault();

            if (league == null)
            {
                League leagueNew = await FillLeague();
                if (LeagueService != null)
                {
                    var actionRes = await LeagueService.AddLeagueAsync(leagueNew);
                    Assert.NotNull(actionRes);
                    Assert.NotNull(actionRes.Result);
                    if (actionRes != null && actionRes.Result != null)
                    {
                        Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
                        Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
                        if (((OkObjectResult)actionRes.Result).Value != null)
                        {
                            League? leagueRet = (League?)((OkObjectResult)actionRes.Result).Value;
                            Assert.NotNull(leagueRet);
                            if (leagueRet != null)
                            {
                                LeagueID = leagueRet.LeagueID;
                            }
                        }
                    }
                }
            }
            else
            {
                LeagueID = league.LeagueID;
            }

            Contact? contact = (from c in db.Contacts
                                select c).FirstOrDefault();

            if (contact == null)
            {
                RegisterModel registerModel = await FillRegisterModelAsync();
                Assert.NotEmpty(registerModel.FirstName);
                Assert.NotEmpty(registerModel.LastName);
                Assert.NotEmpty(registerModel.LoginEmail);

                if (ContactService != null)
                {
                    var actionRes = await ContactService.RegisterAsync(registerModel);
                    Assert.NotNull(actionRes);
                    Assert.NotNull(actionRes.Result);
                    if (actionRes != null && actionRes.Result != null)
                    {
                        Assert.Equal(200, ((ObjectResult)actionRes.Result).StatusCode);
                        Assert.NotNull(((OkObjectResult)actionRes.Result).Value);
                        if (((OkObjectResult)actionRes.Result).Value != null)
                        {
                            Contact? contactRet = (Contact?)((OkObjectResult)actionRes.Result).Value;
                            Assert.NotNull(contactRet);
                            if (contactRet != null)
                            {
                                ContactID = contactRet.ContactID;
                            }
                        }
                    }
                }
            }
            else
            {
                ContactID = contact.ContactID;
            }

            if (LoggedInService != null)
            {
                return await Task.FromResult(new LeagueContact()
                {
                    LeagueContactID = 0,
                    LeagueID = LeagueID,
                    ContactID = ContactID,
                    Removed = false,
                    LastUpdateDate_UTC = DateTime.UtcNow,
                    LastUpdateContactID = 0,
                });
            }
        }

        return await Task.FromResult(new LeagueContact());
    }
}

