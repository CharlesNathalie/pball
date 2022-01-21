namespace PBallModels;

public partial class LeagueGamesModel
{
    public int LeagueID { get; set; } = 0;
    public DateTime StartDate { get; set; } = new DateTime();
    public DateTime EndDate { get; set; } = new DateTime();

    public LeagueGamesModel() 
    {
    }
}

