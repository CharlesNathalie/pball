namespace PBallModels;

public partial class RegisterModel
{
    public string? LoginEmail { get; set; }
    public string? FirstName { get; set; }
    public string? Initial { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
    //public string? ConfirmPassword { get; set; }
    public float PlayerLevel { get; set; }

    public RegisterModel()
    {
    }
}

