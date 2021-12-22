namespace PBallModels;

public partial class LeagueContact : LastUpdate
{
    public int LeagueContactID { get; set; }
    public int LeagueID { get; set; }
    public int ContactID { get; set; }
    public bool Removed { get; set; }

    public LeagueContact() : base()
    {
    }
}


