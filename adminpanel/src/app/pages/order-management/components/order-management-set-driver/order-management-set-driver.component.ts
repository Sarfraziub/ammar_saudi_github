import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { StatusTypeLookup } from 'app/shared/lookup';
import { DriverService, ManageClientOrderService } from 'app/shared/services';

@Component({
  selector: 'order-management-set-driver-cmp',
  templateUrl: './order-management-set-driver.html',
})
export class OrderManagementSetDriverComponent extends BaseComponent {
  @ViewChild('cancelAction') cancelAction: any;

  @Output() onSave = new EventEmitter<boolean>();
  drivers: any[] = [];
  private _selectedItem: any = null;
  @Input() get selectedItem() {
    return this._selectedItem;
  }

  set selectedItem(item: any) {
    if (item) {
      this.clientOrderId.setValue(item.id);
      this.driverId.setValue(item.driverId);
    } else {
      this.onReset(this.form);
    }
  }

  form = new FormGroup({
    clientOrderId: new FormControl(null),
    driverId: new FormControl(null, Validators.required),
  });

  get clientOrderId(): any { return this.form.get('clientOrderId'); }
  get driverId(): any { return this.form.get('driverId'); }

  constructor(private _service: ManageClientOrderService, private driverService: DriverService) {
    super();
    this.drivers = StatusTypeLookup.getLookup;
    this.getDrivers();
  }

  getDrivers() {
    this.drivers = [];
    this.driverService.get()
      .subscribe({
        next: (results) => {
          let items = [];
          results.drivers.forEach(element => {
            items.push(
              { text: element.name || 'الاسم غير معرف', id: element.driverId },
            )
          });
          this.drivers = [...items];
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


    let data = this.setData(this.form);
    data.driverId = +data.driverId;

    let response = this._service.assignDriverForClientOrder(data);

    response.subscribe(() => {
      this.onReset(this.form);
      this.onComplete(this.cancelAction, this.onSave);
    }, error => this.showError(error));
  }

}