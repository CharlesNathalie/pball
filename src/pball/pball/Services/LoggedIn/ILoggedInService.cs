namespace PBallServices;

public interface ILoggedInService
{
    LoggedInContactInfo LoggedInContactInfo { get; set; }
    Task<bool> SetLoggedInContactInfoAsync(string LoginEmail);
}
