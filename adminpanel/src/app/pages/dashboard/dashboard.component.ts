import { Component } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
    selector: 'dashboard',
    templateUrl: './dashboard.html',
})
export class DashboardComponent extends BaseComponent {
    isFullScreen: boolean;

    ngOnInit(): void {

    }
}
