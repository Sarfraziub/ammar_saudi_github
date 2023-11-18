import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DriverFeesType } from 'app/shared/enum';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { DriverFeesService } from 'app/shared/services';

@Component({
    selector: 'driver-configuration',
    templateUrl: './driver-configuration.html',
})
export class DriverConfigurationComponent extends BaseComponent {
    isFullScreen: boolean;

    driverFeesType: typeof DriverFeesType = DriverFeesType;
    form = new FormGroup({

        feeType: new FormControl(1, Validators.required),
        value: new FormControl(null, Validators.required),
    });

    get feeType(): any { return this.form.get('feeType'); }
    get value(): any { return this.form.get('value'); }

    constructor(private _service: DriverFeesService) {
        super();
    }

    ngOnInit(): void {
        this.get();
    }

    get() {
        this._service.get()
            .subscribe({
                next: (result) => {
                    this.form.setValue({ ...this.form.value, ...result });
                    if(this.feeType.value === DriverFeesType.Percentage){
                        this.value.setValue(this.value.value * 100);
                    }
                }, error: (error) => {
                    this.showError(error);
                }
            });
    }

    onSubmit() {
        if (!this.form.valid) {
            this.validateAllFormFields(this.form);
            return;
        }
        let response;

        let value = +this.value.value;
        if (this.feeType.value === DriverFeesType.Percentage) {
            if (value > 100) {
                this.showError('لا يمكن ادخال نسبة اكبر من 100');
                return;
            }
            value = value / 100;
        }


        let data = {
            value: value,
            feeType: +this.feeType.value
        };

        response = this._service.add(data);

        response.subscribe(() => {
            this.get();
        }, error => this.showError(error));
    }
}
