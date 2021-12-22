namespace PBallModels;

public partial class Game : LastUpdate
{
    public int GameID { get; set; }
    public int LeagueID { get; set; }
    public int Player1 { get; set; }
    public int Player2 { get; set; }
    public int Player3 { get; set; }
    public int Player4 { get; set; }
    public int Scores1 { get; set; }
    public int Scores2 { get; set; }
    public int Scores3 { get; set; }
    public int Scores4 { get; set; }
    public DateTime GameDate { get; set; }
    public bool Removed { get; set; }

    public Game() : base()
    {
    }
}


