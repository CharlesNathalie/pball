namespace PBallServices;

public interface ILoggedInService
{
    Task<bool> IsLoggedIn(string LoginEmail);
    List<Contact> LoggedInContactList { get; set; }
}
