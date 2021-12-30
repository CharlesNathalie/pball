namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<Game> FillGame()
    {
        Random random = new Random();

        if (db != null)
        {
            int contactCount = (from c in db.Contacts
                                select c).AsNoTracking().Count();

            if (ContactService != null)
            {
                for (int i = 0; i < (4 - contactCount); i++)
                {
                    RegisterModel registerModel = await FillRegisterModel();
                    registerModel.LoginEmail = "a" + i.ToString() + registerModel.LoginEmail;
                    registerModel.FirstName = registerModel.FirstName + i.ToString();
                    registerModel.LastName = registerModel.LastName + i.ToString();
                    registerModel.Initial = registerModel.Initial + i.ToString();

                    var actionRes = await ContactService.RegisterAsync(registerModel);
                    Contact? contact = await DoOKTestReturnContactAsync(actionRes);
                    Assert.NotNull(contact);

                    if (contact != null)
                    {
                        Assert.True(contact.ContactID > 0);
                    }
                }
            }

            List<Contact> contactList = (from c in db.Contacts
                                         select c).Take(4).ToList();

            Assert.NotEmpty(contactList);
            Assert.Equal(4, contactList.Count());

            League? league = (from c in db.Leagues
                              select c).AsNoTracking().FirstOrDefault();

            if (league == null)
            {
                League? leagueNew = await FillLeague();

                if (LeagueService != null)
                {
                    var actionRes = await LeagueService.AddLeagueAsync(leagueNew);
                    league = await DoOKTestReturnLeagueAsync(actionRes);
                    Assert.NotNull(league);
                    if (league != null)
                    {
                        Assert.True(league.LeagueID > 0);
                    }
                }
            }

            if (LoggedInService != null && LoggedInService.LoggedInContactInfo != null && LoggedInService.LoggedInContactInfo.LoggedInContact != null)
            {
                int Team1Scores = random.Next(1, 11);
                int Team2Scores = random.Next(1, 11);

                if (league != null)
                {
                    return await Task.FromResult(new Game()
                    {
                        Team1Player1 = contactList[0].ContactID,
                        Team1Player2 = contactList[1].ContactID,
                        Team2Player1 = contactList[2].ContactID,
                        Team2Player2 = contactList[3].ContactID,
                        Team1Scores = Team1Scores,
                        Team2Scores = Team2Scores,
                        GameDate = DateTime.Now.AddDays(random.Next(1, 30) * -1),
                        LeagueID = league.LeagueID,
                        Removed = false,
                        LastUpdateDate_UTC = DateTime.UtcNow,
                        LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID,
                    });
                }
            };
        }

        return await Task.FromResult(new Game());
    }
}

