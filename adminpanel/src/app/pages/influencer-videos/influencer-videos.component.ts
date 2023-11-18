import { Component } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
    selector: 'influencer-videos',
    templateUrl: './influencer-videos.html',
})
export class InfluencerVideosComponent extends BaseComponent {
    isFullScreen: boolean;

    ngOnInit(): void {

    }
}
