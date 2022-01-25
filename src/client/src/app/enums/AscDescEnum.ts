import { AppStateService } from "../services/app-state.service";
import { EnumIDAndText } from "../models/EnumIDAndText.model";
import { LanguageEnum } from "./LanguageEnum";

export enum AscDescEnum {
    Ascending = 1,
    Descending = 2,
}

export function GetAscDescEnum(): typeof AscDescEnum {
    return AscDescEnum;
}

export function AscDescEnum_GetOrderedText(state: AppStateService): EnumIDAndText[] {
    let enumTextOrderedList: EnumIDAndText[] = [];
    if (state.Language == LanguageEnum.fr) {
        enumTextOrderedList.push({ EnumID: 1, EnumText: 'Ascendant' });
        enumTextOrderedList.push({ EnumID: 2, EnumText: 'Descendant' });
    }
    else {
        enumTextOrderedList.push({ EnumID: 1, EnumText: 'Ascending' });
        enumTextOrderedList.push({ EnumID: 2, EnumText: 'Descending' });
    }

    return enumTextOrderedList.sort((a, b) => a.EnumText.localeCompare(b.EnumText));
}

export function AscDescEnum_GetIDText(enumID: number, state: AppStateService): string {
    let AscDescEnumText: string = '';
    AscDescEnum_GetOrderedText(state).forEach(e => {
        if (e.EnumID == enumID) {
            AscDescEnumText = e.EnumText;
            return false;
        }
        return AscDescEnumText;
    });

    return AscDescEnumText;
}
