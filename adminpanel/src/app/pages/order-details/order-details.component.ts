import { Component } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
    selector: 'order-details',
    templateUrl: './order-details.html',
})
export class OrderDetailsComponent extends BaseComponent {
    isFullScreen: boolean;

    ngOnInit(): void {

    }
}
