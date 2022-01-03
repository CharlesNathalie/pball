import { LastUpdate } from "./LastUpdate.model";

export class LeagueContact extends LastUpdate {
    LeagueContactID: number = 0;
    LeagueID: number = 0;
    ContactID: number = 0;
    IsLeagueAdmin: boolean = false;
    Removed: boolean = false;
}