import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { AppStateService } from './app-state.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  destroyed = new Subject<void>();

  constructor(private state: AppStateService,
    breakpointObserver: BreakpointObserver) {
    breakpointObserver
      .observe([
        Breakpoints.XSmall,
        Breakpoints.Small,
        Breakpoints.Medium,
        Breakpoints.Large,
        Breakpoints.XLarge,
        Breakpoints.Handset,
        Breakpoints.Tablet,
        Breakpoints.Web,
        Breakpoints.HandsetPortrait,
        Breakpoints.TabletPortrait,
        Breakpoints.WebPortrait,
        Breakpoints.HandsetLandscape,
        Breakpoints.TabletLandscape,
        Breakpoints.WebLandscape,
      ])
      .pipe(takeUntil(this.destroyed))
      .subscribe(result => {
        if (!result.matches) {
          this.state.CurrentCols = '1';
        }
        else {
          for (const query of Object.keys(result.breakpoints)) {
            if (result.breakpoints[query]) {
              switch (query) {
                case '(max-width: 599.98px)':
                  {
                    this.state.CurrentCols = '1';
                    this.state.Screen = 'Small';
                  }
                  break;
                case '(max-width: 599.98px) and (orientation: portrait)':
                  {
                    this.state.CurrentCols = '1';
                    this.state.Screen = 'Small';
                  }
                  break;
                case '(max-width: 959.98px) and (orientation: landscape)':
                  {
                    this.state.CurrentCols = '2';
                    this.state.Screen = 'Small';
                  }
                  break;
                case '(min-width: 600px) and (max-width: 839.98px) and (orientation: portrait':
                  {
                    this.state.CurrentCols = '2';
                    this.state.Screen = 'Small';
                  }
                  break;
                case '(min-width: 600px) and (max-width: 959.98px)':
                  {
                    this.state.CurrentCols = '2';
                    this.state.Screen = 'Small';
                  }
                  break;
                case '(min-width: 840px) and (orientation: portrait':
                  {
                    this.state.CurrentCols = '2';
                    this.state.Screen = 'Large';
                  }
                  break;
                case '(min-width: 960px) and (max-width: 1279.98px)':
                  {
                    this.state.CurrentCols = '2';
                    this.state.Screen = 'Large';
                  }
                  break;
                case '(min-width: 960px) and (max-width: 1279.98px) and (orientation: landscape)':
                  {
                    this.state.CurrentCols = '2';
                    this.state.Screen = 'Large';
                  }
                  break;
                case '(min-width: 1280px) and (max-width: 1919.98px)':
                  {
                    this.state.CurrentCols = '2';
                    this.state.Screen = 'Large';
                  }
                  break;
                case '(min-width: 1280px) and (orientation: landscape)':
                  {
                    this.state.CurrentCols = '2';
                    this.state.Screen = 'Large';
                  }
                  break;
                case '(min-width: 1920px)':
                  {
                    this.state.CurrentCols = '2';
                    this.state.Screen = 'Large';
                  }
                  break;
                default:
                  {
                    this.state.CurrentCols = '1';
                    this.state.Screen = 'Small';
                  }
                  break;
              }
            }
          }
        }
      });
  }


  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.destroyed.next();
    this.destroyed.complete();
  }


}
