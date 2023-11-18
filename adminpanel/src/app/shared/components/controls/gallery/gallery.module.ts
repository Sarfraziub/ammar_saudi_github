import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Gallery } from './gallery.component';

export {
    Gallery,
};

@NgModule({
    imports: [CommonModule, FormsModule],
    declarations: [
        Gallery,
    ],
    exports: [Gallery]
})
export class GalleryModule { }
