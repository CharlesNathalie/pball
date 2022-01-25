namespace PBallModels;

public partial class League : LastUpdate
{
    public int LeagueID { get; set; } = 0;
    public string LeagueName { get; set; } = string.Empty;
    public double PointsToWinners { get; set; } = 0;
    public double PointsToLosers { get; set; } = 0;
    public double PlayerLevelFactor { get; set; } = 0;
    public double PercentPointsFactor { get; set; } = 0;
    public bool Removed { get; set; } = false;

    public League() : base()
    {
    }
}


