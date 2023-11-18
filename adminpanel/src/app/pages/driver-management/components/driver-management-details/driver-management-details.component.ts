import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { DriverService, UploadService } from 'app/shared/services';
import { catchError, forkJoin, of, switchMap } from 'rxjs';

@Component({
  selector: 'driver-management-details-cmp',
  templateUrl: './driver-management-details.html',
})
export class DriverManagementDetailsComponent extends BaseComponent {
  @ViewChild('cancelAction') cancelAction: any;

  @Output() onSave = new EventEmitter<boolean>();
  private _selectedItem: any = null;
  @Input() get selectedItem() {
    return this._selectedItem;
  }

  set selectedItem(item: any) {
    this.isNationalImageUploaded = false;
    this.isIbanImageUploaded = false;
    this.ibanImage = null;
    this.nationalImage = null;
    if (item) {
      this.getById(item.driverId);
    } else {
      this.onReset(this.form);
    }
  }

  form = new FormGroup({
    driverId: new FormControl(null),
    name: new FormControl('', Validators.required),
    phoneNumber: new FormControl('', Validators.required),
    email: new FormControl('', Validators.compose([Validators.required, Validators.email])),
    bankName: new FormControl('', Validators.required),
    iban: new FormControl('', Validators.required),
    nationalId: new FormControl('', Validators.required),
    nationalImageImageId: new FormControl(null),
    ibanImageId: new FormControl(null),
    imageId: new FormControl(null),
    imageUrl: new FormControl(null),
    username: new FormControl(null),
    locked: new FormControl(null),
    ibanUrl: new FormControl(''),
    nationalIdUrl: new FormControl(''),
    bankNameUrl: new FormControl(''),
    activatedDate: new FormControl(''),
    activeDriver: new FormControl(''),
  });

  get driverId(): any { return this.form.get('driverId'); }
  get email(): any { return this.form.get('email'); }
  get name(): any { return this.form.get('name'); }
  get phoneNumber(): any { return this.form.get('phoneNumber'); }
  get bankName(): any { return this.form.get('bankName'); }
  get iban(): any { return this.form.get('iban'); }
  get nationalId(): any { return this.form.get('nationalId'); }

  get ibanUrl(): any { return this.form.get('ibanUrl'); }
  get nationalIdUrl(): any { return this.form.get('nationalIdUrl'); }

  get ibanImageId(): any { return this.form.get('ibanImageId'); }
  get nationalImageImageId(): any { return this.form.get('nationalImageImageId'); }

  isNationalImageUploaded: boolean;
  nationalImage: any = null;

  isIbanImageUploaded: boolean;
  ibanImage: any = null;

  constructor(private _uploadService: UploadService, private _service: DriverService, private _sanitizer: DomSanitizer) {
    super();
  }

  onNationalImageFileChange(event) {
    this.nationalIdUrl.setValue(event.target.files[0]);
    this.handleFileSelect(event);
  }

  private handleFileSelect(event) {
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
    this.isNationalImageUploaded = true;
    this.nationalImage = this._sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
      + btoa(binaryString));
  }

  getById(id) {
    this._service.getById(id)
      .subscribe({
        next: (result) => {
          this.form.setValue({ ...this.form.value, ...result });
          this.ibanImage = result.ibanUrl;
          this.nationalImage = result.nationalIdUrl;
        }, error: (error) => {
          this.showError(error);
        }
      });
  }

  onIbanFileChange(event) {
    this.ibanUrl.setValue(event.target.files[0]);
    this.handleIbanSelect(event);
  }

  private handleIbanSelect(event) {
    var files = event.target.files;
    var file = files[0];

    if (files && file) {
      var reader = new FileReader();

      reader.onload = this.handleReaderLoadedForIban.bind(this);

      reader.readAsBinaryString(file);
    }
  }

  private handleReaderLoadedForIban(reader) {
    var binaryString = reader.target.result;
    this.isIbanImageUploaded = true;
    this.ibanImage = this._sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
      + btoa(binaryString));
  }

  onSubmit() {
    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return;
    }



    let forkJoinObs: any = {

    };

    let ibanImage = this.isIbanImageUploaded ? this._uploadService.upload(this.ibanUrl.value).pipe(catchError(() => of())) : null;
    let natoinalImage = this.isNationalImageUploaded ? this._uploadService.upload(this.nationalIdUrl.value).pipe(catchError(() => of())) : null;

    if (!ibanImage && !natoinalImage) {
      this.submit();
      return;
    }

    if (ibanImage)
      forkJoinObs.ibanImage = ibanImage;

    if (natoinalImage)
      forkJoinObs.natoinalImage = natoinalImage;


    of(forkJoin(forkJoinObs)).pipe(
      switchMap(value => value)).subscribe({
        next: (response: any) => {
          this.ibanImageId.setValue(response.ibanImage);
          this.nationalImageImageId.setValue(response.natoinalImage);

        },
        error: (e) => this.showError(e),
        complete: () => this.submit(),

      });
  }

  private submit() {

    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return;
    }

    let response;
    if (this.driverId.value) {
      response = this._service.update(this.setData(this.form));
    } else {
      response = this._service.add(this.setData(this.form));
    }

    response.subscribe(() => {
      this.onReset(this.form);
      this.isNationalImageUploaded = false;
      this.isIbanImageUploaded = false;
      this.ibanImage = null;
      this.nationalImage = null;
      this.onComplete(this.cancelAction, this.onSave);
    }, error => this.showError(error));
  }

}