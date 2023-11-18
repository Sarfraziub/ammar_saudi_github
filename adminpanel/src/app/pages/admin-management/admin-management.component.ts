import { Component } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
    selector: 'admin-management',
    templateUrl: './admin-management.html',
})
export class AdminManagementComponent extends BaseComponent {
    isFullScreen: boolean;

    ngOnInit(): void {

    }
}
