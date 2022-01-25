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

        double Team1Player1Level = (from c in db.Contacts
                                    where c.ContactID == game.Team1Player1
                                    select c.PlayerLevel).FirstOrDefault();

        double Team1Player2Level = (from c in db.Contacts
                                    where c.ContactID == game.Team1Player2
                                    select c.PlayerLevel).FirstOrDefault();

        double Team2Player1Level = (from c in db.Contacts
                                    where c.ContactID == game.Team2Player1
                                    select c.PlayerLevel).FirstOrDefault();

        double Team2Player2Level = (from c in db.Contacts
                                    where c.ContactID == game.Team2Player2
                                    select c.PlayerLevel).FirstOrDefault();

        int DiffPoints = game.Team1Scores - game.Team2Scores;
        int PercPointsMade = game.Team1Scores / (game.Team1Scores + game.Team2Scores);

        double Player1CalculatedPoints = 0.0D;
        double Player2CalculatedPoints = 0.0D;
        double Player3CalculatedPoints = 0.0D;
        double Player4CalculatedPoints = 0.0D;

        if (DiffPoints > 0)
        {
            Player1CalculatedPoints = group.PointsToWinners - (Team1Player1Level - (Team2Player1Level + Team2Player2Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
            Player2CalculatedPoints = group.PointsToWinners - (Team1Player2Level - (Team2Player1Level + Team2Player2Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
            Player3CalculatedPoints = group.PointsToLosers - (Team2Player1Level - (Team1Player1Level + Team1Player2Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
            Player4CalculatedPoints = group.PointsToLosers - (Team2Player2Level - (Team1Player1Level + Team1Player2Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
        }
        else
        {
            Player1CalculatedPoints = group.PointsToLosers - (Team1Player1Level - (Team2Player1Level + Team2Player2Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
            Player2CalculatedPoints = group.PointsToLosers - (Team1Player2Level - (Team2Player1Level + Team2Player2Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
            Player3CalculatedPoints = group.PointsToWinners - (Team2Player1Level - (Team1Player1Level + Team1Player2Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
            Player4CalculatedPoints = group.PointsToWinners - (Team2Player2Level - (Team1Player1Level + Team1Player2Level) / 2) * (group.PlayerLevelFactor - PercPointsMade * group.PercentPointsFactor);
        }

        calculatedPointList.Add(Player1CalculatedPoints);
        calculatedPointList.Add(Player2CalculatedPoints);
        calculatedPointList.Add(Player3CalculatedPoints);
        calculatedPointList.Add(Player4CalculatedPoints);

        return await Task.FromResult(calculatedPointList);
    }
}

