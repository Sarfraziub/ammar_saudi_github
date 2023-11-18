import { Component, ViewChild } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { SystemSettingsService } from 'app/shared/services';
import { AboutUsComponent, ContactUsComponent } from './components';

@Component({
  selector: 'app-system-setting',
  templateUrl: './system-setting.component.html',
})
export class SystemSettingComponent extends BaseComponent {
  @ViewChild('cancelAction') cancelAction: any;
  @ViewChild('contactUsComponent') contactUsComponent: ContactUsComponent;
  @ViewChild('aboutUsComponent') aboutUsComponent: AboutUsComponent;
  

  results: any = null;
  constructor(private _service: SystemSettingsService) {
    super();
  }

  ngOnInit(): void {
    this.get();
  }

  get() {
    this._service.get()
      .subscribe({
        next: (results) => {
          this.results = results;
          this.contactUsComponent.setResults(results);
          this.aboutUsComponent.setResults(results);
          if (!results) {
            return;
          }
        }, error: (error) => {
          this.showError(error);
        }
      });
  }


  onSubmit() {
    if (!this.contactUsComponent.contactUsForm.valid || !this.aboutUsComponent.aboutUsForm.valid) {
      this.validateAllFormFields(this.contactUsComponent.contactUsForm);
      return;
    }



    const data = {
      "id": this.results?.id,
      "title": this.results?.title,
      "content": this.aboutUsComponent.content.value,
      "arabicContent": this.aboutUsComponent.arabicContent.value,
      "phone": this.contactUsComponent.phone.value,
      "email": this.contactUsComponent.email.value,
      "address": this.contactUsComponent.address.value,
      "whatsApp": this.contactUsComponent.whatsApp.value,
      "facebook": this.contactUsComponent.facebook.value,
      "instagram": this.contactUsComponent.instagram.value,
      "twitter": this.contactUsComponent.twitter.value,
      "snapchat": this.contactUsComponent.snapchat.value
    }

    this._service.update(data).subscribe(() => {
      this.showSuccess("تمت عملية الحفظ بنجاح");
    }, error => this.showError(error));
  }
}