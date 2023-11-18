import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SelectComponent } from './select.component';

const NGA_COMPONENTS = [
  SelectComponent
];

const NGA_MODULES = [
  CommonModule,
  FormsModule,
  ReactiveFormsModule,
];


@NgModule({
  declarations: [
    ...NGA_COMPONENTS
  ],
  imports: [
    ...NGA_MODULES,
  ],
  exports: [
    ...NGA_COMPONENTS,
  ],
})
export class SelectModule { }
