import { Component } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
    selector: 'slider-management',
    templateUrl: './slider-management.html',
})
export class SliderManagementComponent extends BaseComponent {
    isFullScreen: boolean;

    ngOnInit(): void {

    }
}
