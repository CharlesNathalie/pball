import { Contact } from "src/app/models/Contact.model";
import { League } from "src/app/models/League.model";
import { Game } from "src/app/models/Game.model";
import { LeagueContact } from "src/app/models/LeagueContact.model";

export class WebContact {
    Contact?: Contact;
    LeagueList: League[] = [];
    LeagueContactList: LeagueContact[] = [];
    GameList: Game[] = [];
}