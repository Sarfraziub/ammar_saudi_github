import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { StatusTypeLookup } from 'app/shared/lookup';
import { LocationService, ManageClientOrderService } from 'app/shared/services';

@Component({
  selector: 'order-management-set-location-cmp',
  templateUrl: './order-management-set-location.html',
})
export class OrderManagementSetLocationComponent extends BaseComponent {
  @ViewChild('cancelAction') cancelAction: any;

  @Output() onSave = new EventEmitter<boolean>();
  locations: any[] = [];
  private _selectedItem: any = null;
  @Input() get selectedItem() {
    return this._selectedItem;
  }

  set selectedItem(item: any) {
    if (item) {
      this.clientOrderId.setValue(item.id);
      this.locationId.setValue(item.locationId);
    } else {
      this.onReset(this.form);
    }
  }

  form = new FormGroup({
    clientOrderId: new FormControl(null),
    locationId: new FormControl(null, Validators.required),
  });

  get clientOrderId(): any { return this.form.get('clientOrderId'); }
  get locationId(): any { return this.form.get('locationId'); }

  constructor(private _service: ManageClientOrderService, private locationService: LocationService) {
    super();
    this.locations = StatusTypeLookup.getLookup;
    this.getlocations();
  }

  getlocations() {
    this.locations = [];
    this.locationService.get()
      .subscribe({
        next: (results) => {
          let items = [];
          results.locations.forEach(element => {
            if(element.moreNeeded){
              items.push(
                { text: element.arabicName || 'الاسم غير معرف', id: element.id },
              )  
            }
          });
          this.locations = [...items];
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
    data.locationId = +data.locationId;

    let response = this._service.assignLocationForClientOrder(data);

    response.subscribe(() => {
      this.onReset(this.form);
      this.onComplete(this.cancelAction, this.onSave);
    }, error => this.showError(error));
  }

}