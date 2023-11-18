import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { AdminService } from 'app/shared/services';

@Component({
  selector: 'admin-management-details-cmp',
  templateUrl: './admin-management-details.html',
})
export class AdminManagementDetailsComponent extends BaseComponent {
  @ViewChild('cancelAction') cancelAction: any;

  @Output() onSave = new EventEmitter<boolean>();
  private _selectedItem: any = null;
  @Input() get selectedItem() {
    return this._selectedItem;
  }

  set selectedItem(item: any) {
    if (item) {
      this.form.setValue({ ...this.form.value, ...item });
    } else {
      this.onReset(this.form);
    }
  }

  form = new FormGroup({
    id: new FormControl(null),
    name: new FormControl(''),
    phoneNumber: new FormControl('', Validators.required),
    email: new FormControl(''),
    locked: new FormControl(false),
    username: new FormControl('')

  });

  get id(): any { return this.form.get('id'); }
  get email(): any { return this.form.get('email'); }
  get name(): any { return this.form.get('name'); }
  get phoneNumber(): any { return this.form.get('phoneNumber'); }


  constructor(private _service: AdminService) {
    super();
  }

  onSubmit() {
    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return;
    }

    this._service.add(this.setData(this.form)).subscribe(() => {
      this.onReset(this.form);
      this.onComplete(this.cancelAction, this.onSave);
    }, error => this.showError(error));
  }
}