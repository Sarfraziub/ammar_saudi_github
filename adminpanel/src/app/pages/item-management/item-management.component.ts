import { Component } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
    selector: 'item-management',
    templateUrl: './item-management.html',
})
export class ItemManagementComponent extends BaseComponent {
    isFullScreen: boolean;

    ngOnInit(): void {

    }
}
