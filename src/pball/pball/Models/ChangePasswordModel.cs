namespace PBallModels;

public partial class ChangePasswordModel
{
    public string LoginEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string TempCode { get; set; } = string.Empty;

    public ChangePasswordModel() 
    {

    }
}

