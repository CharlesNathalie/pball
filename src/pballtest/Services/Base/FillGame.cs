namespace pball.Tests;

public partial class BaseServiceTests
{
    protected async Task<Game> FillGame()
    {
        Random random = new Random();

        if (db != null)
        {
            int contactCount = (from c in db.Contacts
                                select c).Count();

            if (ContactService != null)
            {
                for (int i = 0; i < (4 - contactCount); i++)
                {
                    RegisterModel registerModel = await FillRegisterModel();
                    Assert.NotEmpty(registerModel.FirstName);
                    Assert.NotEmpty(registerModel.LastName);
                    Assert.NotEmpty(registerModel.LoginEmail);

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
                        }
                    }
                }
            }

            List<Contact> contactList = (from c in db.Contacts
                                         select c).Take(4).ToList();

            Assert.NotEmpty(contactList);
            Assert.Equal(4, contactList.Count());

            League? league = (from c in db.Leagues
                              select c).FirstOrDefault();

            int LeagueID = 0;

            if (league == null)
            {
                League? leagueNew = await FillLeague();

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

            if (LoggedInService != null && LoggedInService.LoggedInContactInfo != null && LoggedInService.LoggedInContactInfo.LoggedInContact != null)
            {
                int Score1 = random.Next(1, 11);
                int Score2 = random.Next(1, 11);

                return await Task.FromResult(new Game()
                {
                    Player1 = contactList[0].ContactID,
                    Player2 = contactList[0].ContactID,
                    Player3 = contactList[0].ContactID,
                    Player4 = contactList[0].ContactID,
                    Scores1 = Score1,
                    Scores2 = Score1,
                    Scores3 = Score2,
                    Scores4 = Score2,
                    GameDate = DateTime.Now.AddDays(random.Next(1, 30) * -1),
                    LeagueID = LeagueID,
                    Removed = false,
                    LastUpdateDate_UTC = DateTime.UtcNow,
                    LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID,
                });
            };
        }

        return await Task.FromResult(new Game());
    }
}

