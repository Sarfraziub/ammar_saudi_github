import { Component } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
    selector: 'driver-management',
    templateUrl: './driver-management.html',
})
export class DriverManagementComponent extends BaseComponent {
    isFullScreen: boolean;

    ngOnInit(): void {

    }
}
