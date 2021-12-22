namespace PBallModels;

public partial class Contact : LastUpdate
{
    public int ContactID { get; set; }
    public string? LoginEmail { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Initial { get; set; }
    public double PlayerLevel { get; set; }
    public bool IsAdmin { get; set; }
    public string? PasswordHash { get; set; }
    public string? Token { get; set; }
    public bool Removed { get; set; }
    public string? ResetPasswordTempCode { get; set; }

    public Contact() : base()
    {
    }
}


