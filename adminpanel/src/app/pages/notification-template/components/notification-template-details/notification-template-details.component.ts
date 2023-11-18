import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { NotificationTemplateService } from 'app/shared/services';
import { DomSanitizer } from '@angular/platform-browser';


@Component({
  selector: 'notification-template-details-cmp',
  templateUrl: './notification-template-details.html',
})
export class NotificationTemplateDetailsComponent extends BaseComponent {
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
    title: new FormControl('', Validators.required),
    body: new FormControl('', Validators.required),

  });

  get id(): any { return this.form.get('id'); }
  get title(): any { return this.form.get('title'); }
  get body(): any { return this.form.get('body'); }


  constructor(private _service: NotificationTemplateService) {
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