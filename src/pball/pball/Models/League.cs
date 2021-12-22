namespace PBallModels;

public partial class League : LastUpdate
{
    public int LeagueID { get; set; }
    public string? LeagueName { get; set; }
    public int CreatorContactID { get; set; }
    public double PointsToWinners { get; set; }
    public double PointsToLoosers { get; set; }
    public double PlayerLevelFactor { get; set; }
    public double PercentPointsFactor { get; set; }
    public bool Removed { get; set; }

    public League() : base()
    {
    }
}


