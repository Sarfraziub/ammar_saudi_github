import { Component, forwardRef, Injector, Input, ViewEncapsulation } from '@angular/core';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
    selector: 'date-picker',
    templateUrl: './date-picker.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./date-picker.component.scss'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => DatePickerComponent),
            multi: true
        }
    ]

})
export class DatePickerComponent extends BaseComponent implements ControlValueAccessor {

    val: FormControl;
    constructor(private inj: Injector) {
        super();
        this.val = new FormControl();
    }

    ngOnInit(): void {

    }

    get value(): any {
        return this.handleDateOffset(this.val?.value) || null;
    }

    onChange(val?: any) {
        this.val.valueChanges.subscribe(value => {
            this.onChange(this.value)
        });
    }

    onTouch(val?: any) {

    }

    writeValue(value: any) {
        this.val.setValue(value);
        this.onChange(this.value);
    }

    registerOnChange(fn: any) {
        this.onChange = fn;
    }

    registerOnTouched(fn: any) {
        this.onTouch = fn;
    }

    @Input() placeholder: string;
    @Input() type: string = this.calendarType.Date;
    @Input() readonly: boolean;
    
    private _min : Date = null;
    @Input() get min(){
        return new Date(this._min?.setHours(0,0,0,0));
    }
    set min(value){
        this._min = value;
    }

    private _max : Date = null;
    @Input() get max(){
        return new Date(this._max?.setHours(0,0,0,0));
    }
    set max(value){
        this._max = value;
    }

    _disable: boolean
    @Input() set disable(value: boolean) {
        if (value)
            this.val.disable()
        else
            this.val.enable();
        this._disable = value;

    }
    get disable(): boolean {
        return this._disable;
    }

    _required: boolean
    @Input() set required(value: boolean) {
        if (value)
            this.val.setValidators([Validators.required]);
        else
            this.val.removeValidators([Validators.required]);

        this._required = value;
    }

    get required(): boolean {
        return this._required;
    }

    private handleDateOffset(date: string): Date {
        if (isNaN(Date.parse(date))) {
            return null;
        }
        const Offset = (new Date(date).getTimezoneOffset() * -1 / 60);
        const _date: Date = new Date(date);
        _date.setHours(_date.getHours() + Number(Offset));
        return _date;
    }
}
