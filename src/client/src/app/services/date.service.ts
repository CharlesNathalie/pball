import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateService {
  Junk: string[] = ['Junk', 'Junk (fr)'];

  constructor() {
  }

  Convert(d: Date) {
    return (
      d.constructor === Date ? d :
        d.constructor === Array ? new Date(d[0], d[1], d[2]) :
          d.constructor === Number ? new Date(d) :
            d.constructor === String ? new Date(d) :
              typeof d === "object" ? new Date(d.getFullYear(), d.getMonth(), d.getDate()) :
                NaN
    );
  }

  Compare(a: Date, b: Date): number {
    let aa: number = this.Convert(a).valueOf();
    let bb: number = this.Convert(b).valueOf();
    if (!isFinite(aa)) {
      return NaN;
    }
    if (!isFinite(bb)) {
      return NaN;
    }
    let count: number = 0;
    if (aa > bb) {
      count = 1;
    }
    if (aa < bb) {
      count = count - 1;
    }

    return count;
  }

  InRange(d: any, start: any, end: any) {
    return (
      isFinite(d = this.Convert(d).valueOf()) &&
        isFinite(start = this.Convert(start).valueOf()) &&
        isFinite(end = this.Convert(end).valueOf()) ?
        start <= d && d <= end :
        NaN
    );
  }

}
