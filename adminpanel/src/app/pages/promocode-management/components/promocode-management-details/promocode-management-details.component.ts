import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { PromocodeService } from 'app/shared/services';

@Component({
  selector: 'promocode-management-details-cmp',
  templateUrl: './promocode-management-details.html',
})
export class PromoCodeManagementDetailsComponent extends BaseComponent {
  @ViewChild('cancelAction') cancelAction: any;

  min: Date = new Date();
  @Output() onSave = new EventEmitter<boolean>();
  private _selectedItem: any = null;
  @Input() get selectedItem() {
    return this._selectedItem;
  }

  set selectedItem(item: any) {
    if (item) {
      this.form.setValue({ ...this.form.value, ...item });
      this.percentage.setValue(this.percentage.value * 100)
    } else {
      this.onReset(this.form);
    }
  }

  form = new FormGroup({
    id: new FormControl(null),
    name: new FormControl(null),
    code: new FormControl('', Validators.required),
    expiry: new FormControl(new Date(), Validators.required),
    expiryDateFormat: new FormControl(null),
    percentage: new FormControl(0, Validators.required),
    allowUpdate: new FormControl(true)
  });

  get id(): any { return this.form.get('id'); }
  get code(): any { return this.form.get('code'); }
  get expiry(): any { return this.form.get('expiry'); }
  get percentage(): any { return this.form.get('percentage'); }

  constructor(private _service: PromocodeService) {
    super();
  }

  onSubmit() {
    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return;
    }


    let data = this.setData(this.form);
    data.percentage = +data.percentage * 0.01;
    data.expiry = new Date(this.expiry.value.toLocaleString());


    let response;
    if (this.id.value) {
      response = this._service.update(data);
    } else {
      response = this._service.add(data);
    }

    response.subscribe(() => {
      this.onReset(this.form);
      this.onComplete(this.cancelAction, this.onSave);
    }, error => this.showError(error));
  }

}