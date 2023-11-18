import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { UploadService, UserProfileService } from 'app/shared/services';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
})
export class UserProfileComponent extends BaseComponent {
  @ViewChild('cancelAction') cancelAction: any;

  @Output() onSave = new EventEmitter<boolean>();

  profileImage: any = 'assets/avatar-2.png';
  isUploaded: boolean;
  form = new FormGroup({
    name: new FormControl(null),
    ordersCount: new FormControl(null),
    totalSpending: new FormControl(null),
    imageId: new FormControl(null),
    imageUrl: new FormControl(null),
    email: new FormControl(null, Validators.compose([Validators.email])),
    language: new FormControl(null),
    languageName: new FormControl(null),
    street: new FormControl(null),
    city: new FormControl(null),
    state: new FormControl(null),
    countryName: new FormControl(null),
    countryArabicName: new FormControl(null),
    countryId: new FormControl(null),
    zipCode: new FormControl(null)
  });

  get name(): any { return this.form.get('name'); }
  get email(): any { return this.form.get('email'); }
  get imageId(): any { return this.form.get('imageId'); }
  get imageUrl(): any { return this.form.get('imageUrl'); }



  constructor(private _service: UserProfileService, private _uploadService: UploadService, private _sanitizer: DomSanitizer) {
    super();
  }

  ngOnInit(): void {
    this.get();
  }

  get() {
    this._service.get()
      .subscribe({
        next: (results) => {
          this.form.setValue({ ...this.form.value, ...results });
          this.profileImage = results.imageUrl || this.profileImage;
          this._uploadService.setUserInformation(results)
        }, error: (error) => {
          this.showError(error);
        }
      });
  }


  onFileChange(event) {
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
    this.isUploaded = true;
    this.profileImage = this._sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
      + btoa(binaryString));
  }

  onSubmit() {
    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return;
    }

    if (this.isUploaded) {
      this._uploadService.upload(this.imageUrl.value).subscribe(
        (data) => {
          if (data) {
            this.imageId.setValue(data);
          }
          this.updateProfile();
        },
        error => {
          this.showError(error);
        });
    } else {
      this.updateProfile();
    }
  }

  updateProfile() {

    this._service.update(this.setData(this.form)).subscribe(() => {
      this.get();
      this.cancelAction.nativeElement.click();
      this.showSuccess('تم تحديث البيانات بنجاح');
    }, error => this.showError(error));
  }
}