import { LastUpdate } from "./LastUpdate.model";

export class League extends LastUpdate {
    LeagueID: number = 0;
    LeagueName: string = '';
    PointsToWinners: number = 0;
    PointsToLoosers: number = 0;
    PlayerLevelFactor: number = 0;
    PercentPointsFactor: number = 0;
    Removed: boolean = false;
}