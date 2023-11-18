import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TimePickerComponent } from './time-picker.component';
import { DateTimeAdapter, OwlDateTimeIntl, OwlDateTimeModule, OwlNativeDateTimeModule, OWL_DATE_TIME_FORMATS, OWL_DATE_TIME_LOCALE } from 'ng-pick-datetime';
import { MomentDateTimeAdapter } from 'ng-pick-datetime/date-time/adapter/moment-adapter/moment-date-time-adapter.class';

export const CUSTOM_TIME_FORMATS = {
  parseInput: 'HH:mm:ss',
  fullPickerInput: 'HH:mm:ss',
  datePickerInput: 'DD/MM/YYYY',
  timePickerInput: 'HH:mm:ss',
  monthYearLabel: 'MMM YYYY',
  dateA11yLabel: 'LL',
  monthYearA11yLabel: 'MMMM YYYY',
};


export class DefaultIntl extends OwlDateTimeIntl {
  /** A label for the cancel button */
  cancelBtnLabel = 'الغاء';

  /** A label for the set button */
  setBtnLabel = 'ضبط';

  /** A label for the range 'from' in picker info */
  rangeFromLabel = 'من';

  /** A label for the range 'to' in picker info */
  rangeToLabel = 'الى';

  /** A label for the hour12 button (AM) */
  hour12AMLabel = 'AM';

  /** A label for the hour12 button (PM) */
  hour12PMLabel = 'PM';
};


const NGA_COMPONENTS = [
  TimePickerComponent,
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
    { provide: OWL_DATE_TIME_FORMATS, useValue: CUSTOM_TIME_FORMATS },
    { provide: OwlDateTimeIntl, useClass: DefaultIntl },
  ],
})
export class TimePickerModule { }

