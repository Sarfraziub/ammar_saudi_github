import { AfterContentChecked, ChangeDetectionStrategy, ChangeDetectorRef, Component, HostListener, OnInit } from '@angular/core';
import { SwUpdate } from '@angular/service-worker';

import { Subscription } from 'rxjs';
import { TokenStorageService } from './shared/services';
import { EventBusService } from './shared/services/event-bus.service';
import { LoadingService } from './shared/services/loading.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, AfterContentChecked {

  eventBusSub?: Subscription;
  constructor(public loadingService: LoadingService, private ref: ChangeDetectorRef, private tokenStorageService: TokenStorageService, private eventBusService: EventBusService) {

  }
  changeDetection: ChangeDetectionStrategy.OnPush

  ngOnInit(): void {

    this.eventBusSub = this.eventBusService.on('logout', () => {
      this.logout();
    });
  }

  ngAfterContentChecked() {
    this.ref.detectChanges();
  }

  ngOnDestroy(): void {
    if (this.eventBusSub)
      this.eventBusSub.unsubscribe();
  }

  logout(): void {
    this.tokenStorageService.tokenSignOut();
  }

  @HostListener("window:beforeunload", ["$event"]) unloadHandler(event: Event) {
    sessionStorage.removeItem('connected');
  }
}
