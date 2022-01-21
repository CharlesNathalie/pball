import { LastUpdate } from "./LastUpdate.model";

export class Player extends LastUpdate {
    ContactID: number = 0;
    LoginEmail: string = '';
    FirstName: string = '';
    LastName: string = '';
    Initial: string = '';    
    PlayerLevel: number = 0.0;
    Removed: boolean = false;
}
