import { LastUpdate } from "./LastUpdate.model";

export class Game extends LastUpdate {
    GameID: number = 0;
    LeagueID: number = 0;
    Team1Player1: number = 0;
    Team1Player2: number = 0;
    Team2Player1: number = 0;
    Team2Player2: number = 0;
    Team1Scores: number = 0;
    Team2Scores: number = 0;
    GameDate: Date  = new Date(1970, 1, 1);
    Removed: boolean  = false;
}