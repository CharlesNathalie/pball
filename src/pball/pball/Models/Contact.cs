namespace PBallModels;

public partial class Contact : LastUpdate
{
    public int ContactID { get; set; } = 0;
    public string LoginEmail { get; set; } = string.Empty;
    public string FirstName { get; set; }  = string.Empty;
    public string LastName { get; set; }  = string.Empty;
    public string Initial { get; set; }  = string.Empty;
    public double PlayerLevel { get; set; } = 0;
    public bool IsAdmin { get; set; } = false;
    public string PasswordHash { get; set; }  = string.Empty;
    public string Token { get; set; }  = string.Empty;
    public bool Removed { get; set; } = false;
    public string ResetPasswordTempCode { get; set; }  = string.Empty;

    public Contact() : base()
    {

    }
}


