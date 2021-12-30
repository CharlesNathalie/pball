namespace PBallModels;

public partial class Game : LastUpdate
{
    public int GameID { get; set; } = 0;
    public int LeagueID { get; set; } = 0;
    public int Team1Player1 { get; set; } = 0;
    public int Team1Player2 { get; set; } = 0;
    public int Team2Player1 { get; set; } = 0;
    public int Team2Player2 { get; set; } = 0;
    public int Team1Scores { get; set; } = 0;
    public int Team2Scores { get; set; } = 0;
    public DateTime GameDate { get; set; } = new DateTime();
    public bool Removed { get; set; } = false;

    public Game() : base()
    {
    }
}


