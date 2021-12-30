namespace PBallModels;

public partial class LeagueContact : LastUpdate
{
    public int LeagueContactID { get; set; } = 0;
    public int LeagueID { get; set; } = 0;
    public int ContactID { get; set; } = 0;
    public bool IsLeagueAdmin { get; set; } = false;
    public bool Removed { get; set; } = false;

    public LeagueContact() : base()
    {
    }
}


