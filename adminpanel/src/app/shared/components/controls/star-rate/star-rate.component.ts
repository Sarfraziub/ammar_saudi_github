import { Component, Injector, Input, ViewEncapsulation } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
    selector: 'star-rate',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./star-rate.component.scss'],
    templateUrl: './star-rate.html',
})
export class StarRateComponent extends BaseComponent {

    private _value: number = 0;
    @Input() get value() {
        return this._value ? this._value : 0;
    }
    set value(value) {
        if (value) {
            this._value = value;
        }
    }

    counter(i: number) {
        return new Array(i);
    }

    handleStar(index) {
        return (index + 1) <= this.value && this.value > 0;
    }

}


