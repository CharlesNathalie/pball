import { HttpErrorResponse } from "@angular/common/http";
import { LanguageEnum } from "../enums/LanguageEnum";
import { Contact } from "./Contact.model";

export class AppState {
    Working?: boolean;
    Error?: HttpErrorResponse;
    Status?: string;

    BaseApiUrl?: string;
    Language?: LanguageEnum;
    Contact?: Contact;
}
