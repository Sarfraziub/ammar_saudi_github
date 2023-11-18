import { Component, forwardRef, Input, ViewEncapsulation } from '@angular/core';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
    selector: 'select-cmp',
    templateUrl: './select.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./select.component.scss'],
})
export class SelectComponent extends BaseComponent implements ControlValueAccessor {

    val: FormControl;
    arrowFocus: boolean;
    searchFocus: boolean;
    viewDetails: boolean;
    constructor() {
        super();
        this.val = new FormControl();
    }

    options: any[] = [];

    ngOnInit(): void {
        this.options = [
            {
                id: 1,
                text: 'One'
            },
            {
                id: 2,
                text: 'Two'
            },
            {
                id: 3,
                text: 'Three'
            },
            {
                id: 4,
                text: 'Four'
            }
        ]
    }

    get value(): any {
        return this.val?.value || null;
    }

    onChange(_val?: any) {
        this.val.valueChanges.subscribe(_value => {
            this.onChange(this.value)
        });
    }

    onTouch(_val?: any) {

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

    get renderStyle() {
        const element = document.getElementById("element-items");

        const elementRect = element.getBoundingClientRect();

        const spaceAbove = elementRect.top;
        const spaceBelow = window.innerHeight - elementRect.bottom;

        let x = document.getElementsByClassName("input-group")[0].clientHeight;

        if (spaceBelow < spaceAbove) {
            return `z-index: 1001;
            transform-origin: center bottom;
            top: -${x + 200}px;
            left: 0px;`;
            // logic to render with more space above input
        } else {
            return `z-index: 1001;
            transform-origin: center top;
            top: ${x};
            left: 0px;`;
            // logic to render with more (or equal) space on bottom
        }
    }
}
