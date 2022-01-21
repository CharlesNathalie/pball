namespace PBallModels;

public partial class Player
{
    public int ContactID { get; set; } = 0;
    public string LoginEmail { get; set; } = string.Empty;
    public string FirstName { get; set; }  = string.Empty;
    public string LastName { get; set; }  = string.Empty;
    public string Initial { get; set; }  = string.Empty;
    public double PlayerLevel { get; set; } = 0;
    public bool Removed { get; set; } = false;

    public Player()
    {

    }
}


