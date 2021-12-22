namespace PBallModels;

public partial class ChangePasswordModel
{
    public string? LoginEmail { get; set; }
    public string? Password { get; set; }
    public string? TempCode { get; set; }

    public ChangePasswordModel() 
    {
    }
}

