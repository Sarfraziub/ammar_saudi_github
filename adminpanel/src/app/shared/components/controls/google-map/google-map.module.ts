import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GoogleMapComponent } from './google-map.component';
import { GoogleMapsModule } from '@angular/google-maps';

export {
    GoogleMapComponent,
};

@NgModule({
    imports: [CommonModule, FormsModule, GoogleMapsModule],
    declarations: [
        GoogleMapComponent
    ],
    exports: [GoogleMapComponent]
})
export class AppGoogleMapModule { }
