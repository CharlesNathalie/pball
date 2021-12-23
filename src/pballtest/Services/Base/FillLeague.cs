namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    protected async Task<League> FillLeague()
    {
        Random random = new Random();

        int CreatorContactID = 0;
        if (db != null)
        {
            Contact? contact = (from c in db.Contacts
                                select c).FirstOrDefault();

            if (contact == null)
            {
                RegisterModel registerModel = await FillRegisterModel();
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
                                CreatorContactID = contactRet.ContactID;
                            }
                        }
                    }
                }
            }
            else
            {
                CreatorContactID = contact.ContactID;
            }

            if (LoggedInService != null && LoggedInService.LoggedInContactInfo != null && LoggedInService.LoggedInContactInfo.LoggedInContact != null)
            {
                return await Task.FromResult(new League()
                {
                    LeagueName = $"League Name { random.Next(1, 1000) }",
                    CreatorContactID = CreatorContactID,
                    PercentPointsFactor = 1.1D,
                    PlayerLevelFactor = 1.1D,
                    PointsToLoosers = 1.1D,
                    PointsToWinners = 1.1D,
                    Removed = false,
                    LastUpdateDate_UTC = DateTime.UtcNow,
                    LastUpdateContactID = LoggedInService.LoggedInContactInfo.LoggedInContact.ContactID,
                });
            }
        }

        return await Task.FromResult(new League());
    }
}

