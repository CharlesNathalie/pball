import { Player } from "./Player.model";

export class User extends Player {
    IsAdmin: boolean = false;
    Token: string = '';
}
