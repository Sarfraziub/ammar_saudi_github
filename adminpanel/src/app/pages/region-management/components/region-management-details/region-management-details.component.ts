import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { RegionService } from 'app/shared/services';

@Component({
  selector: 'region-management-details-cmp',
  templateUrl: './region-management-details.html',
})
export class RegionManagementDetailsComponent extends BaseComponent implements OnInit {
  @ViewChild('cancelAction') cancelAction: any;
  locations: any[] = [];

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
    name: new FormControl('', Validators.required),
    arabicName: new FormControl('', Validators.required),
  });

  get id(): any { return this.form.get('id'); }
  get name(): any { return this.form.get('name'); }
  get arabicName(): any { return this.form.get('arabicName'); }


  constructor(private _service: RegionService) {
    super();
  }

  ngOnInit(): void {
  }

  onSubmit() {
    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return;
    }

    let response;
    if (this.id.value) {
      response = this._service.update(this.setData(this.form));
    } else {
      response = this._service.add(this.setData(this.form));
    }

    response.subscribe(() => {
      this.onReset(this.form);
      this.onComplete(this.cancelAction, this.onSave);
    }, error => this.showError(error));
  }
}