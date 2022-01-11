namespace PBallModels;

public partial class MostGamesPlayedModel
{
    public int ContactID { get; set; } = 0;
    public string FullName { get; set; } = "";
    //public string LastName { get; set; } = "";
    //public string Initial { get; set; } = "";
    public int NumberOfGames { get; set; } = 0;

    public MostGamesPlayedModel() : base()
    {
    }
}


