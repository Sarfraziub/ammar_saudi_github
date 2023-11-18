import { Component } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
    selector: 'notification-template',
    templateUrl: './notification-template.html',
})
export class NotificationTemplateComponent extends BaseComponent {
    isFullScreen: boolean;

    ngOnInit(): void {

    }
}
