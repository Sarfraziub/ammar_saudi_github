import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { StarRateComponent } from './star-rate.component';

export {
    StarRateComponent,
};

@NgModule({
    imports: [CommonModule, FormsModule],
    declarations: [
        StarRateComponent,
    ],
    exports: [StarRateComponent]
})
export class StartRateModule { }
