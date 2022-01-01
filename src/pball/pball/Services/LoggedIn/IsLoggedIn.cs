namespace PBallServices;

public partial class LoggedInService : ILoggedInService
{
    public async Task<bool> IsLoggedIn(string LoginEmail)
    {
        return await Task.FromResult(LoggedInContactList.Where(c => c.LoginEmail == LoginEmail).Any());
    }
}

