import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { PackageService, UploadService } from 'app/shared/services';

@Component({
  selector: 'package-management-details-cmp',
  templateUrl: './package-management-details.html',
})
export class PackageManagementDetailsComponent extends BaseComponent {
  @ViewChild('cancelAction') cancelAction: any;
  isItemUploaded: boolean;
  itemImage: any = null;
  @Output() onSave = new EventEmitter<boolean>();
  private _selectedItem: any = null;
  @Input() get selectedItem() {
    return this._selectedItem;
  }

  set selectedItem(item: any) {
    this.isItemUploaded = false;
    this.itemImage = null;
    if (item) {
      this.form.setValue({ ...this.form.value, ...item });
      this.itemImage = item.imageUrl || this.itemImage;

    } else {
      this.onReset(this.form);
      this.visible.setValue(true);
    }
  }

  form = new FormGroup({
    id: new FormControl(null),
    name: new FormControl('', Validators.required),
    arabicName: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
    arabicDescription: new FormControl('', Validators.required),
    visible: new FormControl([true, Validators.required]),
    fileId: new FormControl(null),
    imageUrl: new FormControl(null),
  });

  get id(): any { return this.form.get('id'); }
  get name(): any { return this.form.get('name'); }
  get arabicName(): any { return this.form.get('arabicName'); }
  get description(): any { return this.form.get('description'); }
  get arabicDescription(): any { return this.form.get('arabicDescription'); }
  get fileId(): any { return this.form.get('fileId'); }
  get imageUrl(): any { return this.form.get('imageUrl'); }
  get visible(): any { return this.form.get('visible'); }


  constructor(private _sanitizer: DomSanitizer, private _service: PackageService, private _uploadService: UploadService) {
    super();
  }

  onItemFileChange(event) {
    this.imageUrl.setValue(event.target.files[0]);
    this.handleFileSelect(event);
  }

  handleFileSelect(event) {
    var files = event.target.files;
    var file = files[0];

    if (files && file) {
      var reader = new FileReader();

      reader.onload = this.handleReaderLoaded.bind(this);

      reader.readAsBinaryString(file);
    }
  }

  private handleReaderLoaded(reader) {
    var binaryString = reader.target.result;
    this.isItemUploaded = true;
    this.itemImage = this._sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
      + btoa(binaryString));
  }



  onSubmit() {
    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return;
    }


    if (!this.isItemUploaded && !this.id.value) {
      this.showError('يرجى ارفاق صورة');
      return;
    }

    if (this.isItemUploaded) {

      this._uploadService.upload(this.imageUrl.value).subscribe(
        (data) => {
          if (data) {
            this.fileId.setValue(data);
          }
          this.submit();
        },
        error => {
          this.showError(error);
        });
    } else {
      this.submit();
    }

  }

  private submit() {

    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return;
    }

    let data = this.setData(this.form);
    data.visible = data.visible || false;

    let response;
    if (this.id.value) {
      response = this._service.update(data);
    } else {
      response = this._service.add(data);
    }

    response.subscribe(() => {
      this.onReset(this.form);
      this.isItemUploaded = false;
      this.itemImage = null;
      this.onComplete(this.cancelAction, this.onSave);
    }, error => this.showError(error));
  }
}