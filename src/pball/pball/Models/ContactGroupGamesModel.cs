namespace PBallModels;

public partial class ContactLeagueGamesModel
{
    public int ContactID { get; set; }
    public int LeagueID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public ContactLeagueGamesModel() 
    {
    }
}

