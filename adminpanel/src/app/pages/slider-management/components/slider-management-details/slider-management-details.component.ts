import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { SliderItemService, UploadService } from 'app/shared/services';
import { DomSanitizer } from '@angular/platform-browser';


@Component({
  selector: 'slider-management-details-cmp',
  templateUrl: './slider-management-details.html',
})
export class SliderManagementDetailsComponent extends BaseComponent {
  @ViewChild('cancelAction') cancelAction: any;
  isSliderItemUploaded: boolean;
  sliderItemImage: any = null;
  @Output() onSave = new EventEmitter<boolean>();
  private _selectedItem: any = null;
  @Input() get selectedItem() {
    return this._selectedItem;
  }

  set selectedItem(item: any) {
    this.isSliderItemUploaded = false;
    this.sliderItemImage = null;
    if (item) {
      this.form.setValue({ ...this.form.value, ...item });
      this.sliderItemImage = item.imageUrl || this.sliderItemImage;

    } else {
      this.onReset(this.form);
      this.visible.setValue(true);
    }
  }



  form = new FormGroup({
    id: new FormControl(null),
    name: new FormControl('', Validators.required),
    visible: new FormControl([true, Validators.required]),
    order: new FormControl(1, Validators.required),
    created: new FormControl(null),
    src: new FormControl(null),
    date: new FormControl(null),
    imageId: new FormControl(null),
    imageUrl: new FormControl(null),
    title: new FormControl(''),
  });

  get id(): any { return this.form.get('id'); }
  get name(): any { return this.form.get('name'); }
  get visible(): any { return this.form.get('visible'); }
  get order(): any { return this.form.get('order'); }
  get imageId(): any { return this.form.get('imageId'); }
  get imageUrl(): any { return this.form.get('imageUrl'); }

  constructor(private _sanitizer: DomSanitizer, private _service: SliderItemService, private _uploadService: UploadService) {
    super();
  }

  onSliderImageChange(event) {
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
    this.isSliderItemUploaded = true;
    this.sliderItemImage = this._sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
      + btoa(binaryString));
  }

  onSubmit() {
    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return;
    }

    if (!this.isSliderItemUploaded && !this.id.value) {
      this.showError('يرجى ارفاق صورة');
      return;
    }


    if (this.isSliderItemUploaded) {

      this._uploadService.upload(this.imageUrl.value).subscribe(
        (data) => {
          if (data) {
            this.imageId.setValue(data);
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



    if (this.id.value) {
      response = this._service.update(data);
    } else {
      response = this._service.add(data);
    }

    response.subscribe(() => {
      this.onReset(this.form);
      this.isSliderItemUploaded = false;
      this.sliderItemImage = null;

      this.visible.setValue(true);
      this.onComplete(this.cancelAction, this.onSave);
    }, error => this.showError(error));
  }
}