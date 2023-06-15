import { formatCurrency, formatDate, formatNumber } from '@angular/common';
//import { regexType } from './regexDefault';
import * as moment from 'moment';

export abstract class CommondParent {

    static globalLocale: string;
    //regexType = regexType;
    moment = moment;

    constructor() {
    }

    // formats
    formatNumber(text: number, digitsInfo?: string) {
        return formatNumber(text, CommondParent.globalLocale, digitsInfo);
    }

    formatCurrency(text: number) {
        return formatCurrency(text, CommondParent.globalLocale, '$');
    }

    formatCurrency0(text: number) {
        return formatCurrency(text, CommondParent.globalLocale, '$', undefined, '1.0-0');
    }

    formatDate(text: string) {
        return formatDate(text, 'dd/MM/yyyy', CommondParent.globalLocale);
    }


    formatDateTimeShort(text: string) {
         return  this.moment(text,'HH:mm').format('hh:mm')
    }


    formatDateHttpGet(text: string) {
        return formatDate(text, 'yyyy-MM-dd', CommondParent.globalLocale);
    }

    formatDateShort(text: string) {
        return formatDate(text, 'dd \'de\' MMMM yyyy', CommondParent.globalLocale);
    }
    
}
