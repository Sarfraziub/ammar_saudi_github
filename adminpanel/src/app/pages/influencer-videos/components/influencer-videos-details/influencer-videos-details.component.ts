import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { HomePageIconsService, InfluencerVideosService, UploadService } from 'app/shared/services';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'influencer-videos-details-cmp',
  templateUrl: './influencer-videos-details.html',
})

export class InfluencerVideosDetailsComponent extends BaseComponent {
  @ViewChild('cancelAction') cancelAction: any;
  isFileItemUploaded: boolean;
  uploadedItemFile: any = null;
  @Output() onSave = new EventEmitter<boolean>();
  private _selectedItem: any = null;
  format = null;
  @Input() get selectedItem() {
    return this._selectedItem;
  }

  set selectedItem(item: any) {
    this.isFileItemUploaded = false;
    if (item) {
      this.form.setValue({ ...this.form.value, ...item });
      this.uploadedItemFile = item.imageUrl || this.uploadedItemFile;
      this.format = this.contentType.value === 1 ? 'image' : 'video';

    } else {
      this.onReset(this.form);
      this.visible.setValue(true);
      this.format = null;
      this.isFileItemUploaded = false;
      this.uploadedItemFile = null;
    }
  }

  form = new FormGroup({
    id: new FormControl(null),
    title: new FormControl('', Validators.required),
    content: new FormControl('', Validators.required),
    fileId: new FormControl(null),
    imageUrl: new FormControl(null),
    src: new FormControl(null),
    date: new FormControl(null),
    visible: new FormControl([true, Validators.required]),
    contentType: new FormControl([1]),
  });

  get id(): any { return this.form.get('id'); }
  get title(): any { return this.form.get('title'); }
  get content(): any { return this.form.get('content'); }
  get src(): any { return this.form.get('src'); }
  get date(): any { return this.form.get('date'); }
  get fileId(): any { return this.form.get('fileId'); }
  get imageUrl(): any { return this.form.get('imageUrl'); }
  get visible(): any { return this.form.get('visible'); }
  get contentType(): any { return this.form.get('contentType'); }



  constructor(private _sanitizer: DomSanitizer, private _service: InfluencerVideosService, private _uploadService: UploadService) {
    super();
  }

  onSelectFile(event) {
    this.imageUrl.setValue(event.target.files[0]);
    const file = event.target.files && event.target.files[0];
    if (file) {
      var reader = new FileReader();
      reader.readAsDataURL(file);
      if (file.type.indexOf('image') > -1) {
        this.format = 'image';
      } else if (file.type.indexOf('video') > -1) {
        this.format = 'video';
      }
      reader.onload = (event) => {
        this.uploadedItemFile = (<FileReader>event.target).result;
        this.isFileItemUploaded = true
      }
    }
  }

  onSubmit() {
    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return;
    }

    if (!this.isFileItemUploaded && !this.id.value) {
      this.showError('يرجى ارفاق محتوى ملف');
      return;
    }


    if (this.isFileItemUploaded) {

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

  submit() {

    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return;
    }

    let response;

    let data = this.setData(this.form);
    data.visible = data.visible || false;
    data.contentType = this.format === 'image' ? 1 : 2;



    if (this.id.value) {
      response = this._service.update(data);
    } else {
      response = this._service.add(data);
    }

    response.subscribe(() => {
      this.onReset(this.form);
      this.visible.setValue(true);
      this.format = null;
      this.isFileItemUploaded = false;
      this.uploadedItemFile = null;
      this.onComplete(this.cancelAction, this.onSave);
    }, error => this.showError(error));
  }
}