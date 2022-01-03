namespace PBallServices;

public partial class LoggedInService : ILoggedInService
{
    public List<Contact> LoggedInContactList { get; set; } = new List<Contact>();

    public LoggedInService()
    {

    }
}

