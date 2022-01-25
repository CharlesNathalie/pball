
import { EnumIDAndText } from 'src/app/models/EnumIDAndText.model';
import { AppStateService } from '../services/app-state.service';

export enum LanguageEnum {
    en = 1,
    fr = 2,
}

export function GetLanguageEnum(): typeof LanguageEnum {
    return LanguageEnum;
}

export function LanguageEnum_GetOrderedText(state: AppStateService): EnumIDAndText[] {
    let enumTextOrderedList: EnumIDAndText[] = [];
    if (state.Language == LanguageEnum.fr) {
        enumTextOrderedList.push({ EnumID: 1, EnumText: 'en' });
        enumTextOrderedList.push({ EnumID: 2, EnumText: 'fr' });
    }
    else {
        enumTextOrderedList.push({ EnumID: 1, EnumText: 'en' });
        enumTextOrderedList.push({ EnumID: 2, EnumText: 'fr' });
    }

    return enumTextOrderedList.sort((a, b) => a.EnumText.localeCompare(b.EnumText));
}

export function LanguageEnum_GetIDText(enumID: number, state: AppStateService): string {
    let LanguageEnumText: string = '';
    LanguageEnum_GetOrderedText(state).forEach(e => {
        if (e.EnumID == enumID) {
            LanguageEnumText = e.EnumText;
            return false;
        }
        return LanguageEnumText;
    });

    return LanguageEnumText;
}
