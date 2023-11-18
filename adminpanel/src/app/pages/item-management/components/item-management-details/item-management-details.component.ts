import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { ItemService, PackageService, UploadService } from 'app/shared/services';

@Component({
  selector: 'item-management-details-cmp',
  templateUrl: './item-management-details.html',
})
export class ItemManagementDetailsComponent extends BaseComponent implements OnInit {
  @ViewChild('cancelAction') cancelAction: any;
  isItemUploaded: boolean;
  packages: any[] = [];
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
    }
  }

  form = new FormGroup({
    id: new FormControl(null),
    name: new FormControl('', Validators.required),
    arabicName: new FormControl('', Validators.required),
    specifications: new FormControl('', Validators.required),
    arabicSpecifications: new FormControl('', Validators.required),
    packageId: new FormControl(null, Validators.required),
    price: new FormControl('', Validators.required),
    imageId: new FormControl(null),
    imageUrl: new FormControl(null),
    quantity: new FormControl(null),
    packageName: new FormControl(null),
    packageDescription: new FormControl(null),
    packageArabicName: new FormControl(null),
    packageArabicDescription: new FormControl(null),
  });

  get id(): any { return this.form.get('id'); }
  get name(): any { return this.form.get('name'); }
  get arabicName(): any { return this.form.get('arabicName'); }
  get specifications(): any { return this.form.get('specifications'); }
  get arabicSpecifications(): any { return this.form.get('arabicSpecifications'); }
  get packageId(): any { return this.form.get('packageId'); }
  get price(): any { return this.form.get('price'); }
  get imageId(): any { return this.form.get('imageId'); }
  get imageUrl(): any { return this.form.get('imageUrl'); }

  constructor(private _sanitizer: DomSanitizer, private _service: ItemService, private _packageService: PackageService, private _uploadService: UploadService) {
    super();
  }
  ngOnInit(): void {
this.getPackages();
  }

  getPackages() {
    this.packages = [];
    this._packageService.get(true)
      .subscribe({
        next: (results) => {
          this.packages = results.packages.map(m => ({
            text: m.arabicName,
            id: m.id
          }));
        }, error: (error) => {
          this.showError(error);
        }
      });
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

  private submit() {

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
      this.isItemUploaded = false;
      this.itemImage = null;
      this.onComplete(this.cancelAction, this.onSave);
    }, error => this.showError(error));
  }
}