namespace PBallServices;

public partial class LoggedInService : ILoggedInService
{
    public async Task<bool> SetLoggedInContactInfoAsync(string LoginEmail)
    {
        LoggedInContactInfo.LoggedInContact = (from c in db.Contacts
                                               where c.LoginEmail == LoginEmail
                                               select c).FirstOrDefault();

        return await Task.FromResult(true);
    }
}

