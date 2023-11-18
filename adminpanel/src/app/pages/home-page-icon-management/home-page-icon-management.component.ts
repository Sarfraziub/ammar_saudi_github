import { Component } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
    selector: 'home-page-icon-management',
    templateUrl: './home-page-icon-management.html',
})
export class HomePageIconManagementComponent extends BaseComponent {
    isFullScreen: boolean;

    ngOnInit(): void {

    }
}
