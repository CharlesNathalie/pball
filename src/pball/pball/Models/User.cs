namespace PBallModels;

public partial class User : Player
{
    public bool IsAdmin { get; set; } = false;
    public string Token { get; set; }  = string.Empty;

    public User() : base()
    {

    }
}


