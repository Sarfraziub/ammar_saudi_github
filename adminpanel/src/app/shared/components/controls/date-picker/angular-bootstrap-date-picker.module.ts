import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DatePickerComponent } from './date-picker.component';
import { DateTimeAdapter, OwlDateTimeModule, OwlNativeDateTimeModule, OWL_DATE_TIME_FORMATS , OWL_DATE_TIME_LOCALE} from 'ng-pick-datetime';
import { MomentDateTimeAdapter } from 'ng-pick-datetime/date-time/adapter/moment-adapter/moment-date-time-adapter.class';

export const CUSTOM_FORMATS = {
  parseInput: 'DD/MM/YYYY HH:mm:ss',
  fullPickerInput: 'DD/MM/YYYY HH:mm:ss',
  datePickerInput: 'DD/MM/YYYY',
  timePickerInput: 'HH:mm:ss',
  monthYearLabel: 'MMM YYYY',
  dateA11yLabel: 'LL',
  monthYearA11yLabel: 'MMMM YYYY',
};

const NGA_COMPONENTS = [
  DatePickerComponent,
];

const NGA_DATE_MODULES = [
  OwlDateTimeModule,
  OwlNativeDateTimeModule,  
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
    ...NGA_DATE_MODULES,
  ],
  exports: [
    ...NGA_COMPONENTS,
    ...NGA_DATE_MODULES,
  ],
  providers: [
    { provide: DateTimeAdapter, useClass: MomentDateTimeAdapter, deps: [OWL_DATE_TIME_LOCALE] },
    { provide: OWL_DATE_TIME_FORMATS, useValue: CUSTOM_FORMATS }
  ],
})
export class DatePickerModule { }
