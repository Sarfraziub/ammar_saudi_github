import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { HomePageIconsService, UploadService } from 'app/shared/services';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'home-page-icon-management-details-cmp',
  templateUrl: './home-page-icon-management-details.html',
})

export class HomePageIconManagementDetailsComponent extends BaseComponent {
  @ViewChild('cancelAction') cancelAction: any;
  isHomePageItemUploaded: boolean;
  homePageItemImage: any = null;
  @Output() onSave = new EventEmitter<boolean>();
  private _selectedItem: any = null;
  @Input() get selectedItem() {
    return this._selectedItem;
  }

  set selectedItem(item: any) {
    this.isHomePageItemUploaded = false;
    if (item) {
      this.homePageItemImage = null;
      this.form.setValue({ ...this.form.value, ...item });
      this.homePageItemImage = item.imageUrl || this.homePageItemImage;

    } else {
      this.onReset(this.form);
      this.visible.setValue(true);

    }
  }

  form = new FormGroup({
    id: new FormControl(null),
    title: new FormControl('', Validators.required),
    arabicTitle: new FormControl('', Validators.required),
    fileId: new FormControl(null),
    imageUrl: new FormControl(null),
    src: new FormControl(null),
    date: new FormControl(null),
    order: new FormControl(null, Validators.required),
    visible: new FormControl([true, Validators.required]),
  });

  get id(): any { return this.form.get('id'); }
  get title(): any { return this.form.get('title'); }
  get arabicTitle(): any { return this.form.get('arabicTitle'); }
  get src(): any { return this.form.get('src'); }
  get date(): any { return this.form.get('date'); }
  get order(): any { return this.form.get('order'); }
  get fileId(): any { return this.form.get('fileId'); }
  get imageUrl(): any { return this.form.get('imageUrl'); }
  get visible(): any { return this.form.get('visible'); }

  constructor(private _sanitizer: DomSanitizer, private _service: HomePageIconsService, private _uploadService: UploadService) {
    super();
  }

  onHomePageImageChange(event) {
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
    this.isHomePageItemUploaded = true;
    this.homePageItemImage = this._sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
      + btoa(binaryString));
  }

  onSubmit() {
    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return;
    }

    // if (!this.isHomePageItemUploaded && !this.id.value) {
    //   this.showError('يرجى ارفاق صورة');
    //   return;
    // }

    this.submit();

    // if (this.isHomePageItemUploaded) {

    //   this._uploadService.upload(this.imageUrl.value).subscribe(
    //     (data) => {
    //       if (data) {
    //         this.fileId.setValue(data);
    //       }
    //       this.submit();
    //     },
    //     error => {
    //       this.showError(error);
    //     });
    // } else {
    //   this.submit();
    // }
  }

  submit() {

    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return;
    }

    let data = this.setData(this.form);
    data.visible = data.visible || false;

    this._service.update(data).subscribe(() => {
      this.onReset(this.form);
      this.homePageItemImage = null;
      this.isHomePageItemUploaded = false;
      this.onComplete(this.cancelAction, this.onSave);
    }, error => this.showError(error));
  }
}