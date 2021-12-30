namespace PBallModels;

public partial class LastUpdate
{
    public DateTime LastUpdateDate_UTC { get; set; } = new DateTime();
    public int LastUpdateContactID { get; set; } = 0;

    public LastUpdate()
    {

    }
}


