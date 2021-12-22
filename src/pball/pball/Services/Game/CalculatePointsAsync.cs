namespace PBallServices;

public partial class GameService : ControllerBase, IGameService
{
    private async Task<List<double>> CalculatePointsAsync(Game game)
    {
        // -------------------------------------------------------------------------------------
        // all this should be in Javascript
        // -------------------------------------------------------------------------------------

        List<double> calculatedPointList = new List<double>();

        League? group = (from c in db.Leagues
                        where c.LeagueID == game.LeagueID
                        select c).FirstOrDefault();

        if (group == null)
        {
            return new List<double>();
        }

        double Player1Level = (from c in db.Contacts
                              where c.ContactID == game.Player1
                              select c.PlayerLevel).FirstOrDefault();

        double Player2Level = (from c in db.Contacts
                              where c.ContactID == game.Player2
                              select c.PlayerLevel).FirstOrDefault();

        double Player3Level = (from c in db.Contacts
                              where c.ContactID == game.Player3
                              select c.PlayerLevel).FirstOrDefault();

        double Player4Level = (from c in db.Contacts
                              where c.ContactID == game.Player4
                              select c.PlayerLevel).FirstOrDefault();

        int DiffPoints = game.Scores1 - game.Scores3;
        int PercPointsMade = game.Scores1 / (game.Scores1 + game.Scores2);

        double Player1CalculatedPoints = 0.0D;
        double Player2CalculatedPoints = 0.0D;
        double Player3CalculatedPoints = 0.0D;
        double Player4CalculatedPoints = 0.0D;

        if (DiffPoints > 0)
        {
            Player1CalculatedPoints = group.PointsToWinners - (Player1Level - (Player3Level + Player4Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
            Player2CalculatedPoints = group.PointsToWinners - (Player2Level - (Player3Level + Player4Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
            Player3CalculatedPoints = group.PointsToLoosers - (Player3Level - (Player1Level + Player2Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
            Player4CalculatedPoints = group.PointsToLoosers - (Player4Level - (Player1Level + Player2Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
        }
        else
        {
            Player1CalculatedPoints = group.PointsToLoosers - (Player1Level - (Player3Level + Player4Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
            Player2CalculatedPoints = group.PointsToLoosers - (Player2Level - (Player3Level + Player4Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
            Player3CalculatedPoints = group.PointsToWinners - (Player3Level - (Player1Level + Player2Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
            Player4CalculatedPoints = group.PointsToWinners - (Player4Level - (Player1Level + Player2Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
        }

        calculatedPointList.Add(Player1CalculatedPoints);
        calculatedPointList.Add(Player2CalculatedPoints);
        calculatedPointList.Add(Player3CalculatedPoints);
        calculatedPointList.Add(Player4CalculatedPoints);

        return await Task.FromResult(calculatedPointList);
    }
}

