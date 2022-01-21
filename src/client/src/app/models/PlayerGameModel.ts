export class PlayerGameModel {
    GameID: number = 0;
    GameDate: Date = new Date();
    PartnerID: number = 0;
    PartnerFullName: string = '';
    PartnerLastNameAndFirstNameInit: string = '';
    Opponent1ID: number = 0;
    Opponent1FullName: string = '';
    Opponent1LastNameAndFirstNameInit: string = '';
    Opponent2ID: number = 0;
    Opponent2FullName: string = '';
    Opponent2LastNameAndFirstNameInit: string = '';
    Won: boolean = false;
    Scores: number = 0;
    OpponentScores: number = 0;

}