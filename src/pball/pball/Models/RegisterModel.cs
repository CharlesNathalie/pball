namespace PBallModels;

public partial class RegisterModel
{
    public string LoginEmail { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Initial { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public double PlayerLevel { get; set; } = 0.0D;

    public RegisterModel()
    {
    }
}

