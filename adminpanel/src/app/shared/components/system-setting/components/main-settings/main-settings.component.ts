import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { EmployeeService } from 'app/shared/services';

@Component({
  selector: 'main-settings-cmp',
  templateUrl: './main-settings.html',
})
export class MainSettingsComponent extends BaseComponent {
  form = new FormGroup({
    id: new FormControl(null),
    numberOfOrders: new FormControl(1, Validators.required),
    closeTime: new FormControl(new Date(), Validators.required),
  });

  get id(): any { return this.form.get('id'); }
  get numberOfOrders(): any { return this.form.get('numberOfOrders'); }
  get closeTime(): any { return this.form.get('closeTime'); }


  constructor(private employeeService: EmployeeService) {
    super();
  }

  onSubmit() {
    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return;
    }

    let response;
    if (this.id.value) {
      response = this.employeeService.update(this.setData(this.form));
    } else {
      response = this.employeeService.add(this.setData(this.form));
    }

    response.subscribe(() => {
      this.showSuccess("تمت عملية الحفظ بنجاح");
    }, error => this.showError(error));
  }

}