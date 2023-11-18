import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
  selector: 'contact-us-cmp',
  templateUrl: './contact-us.html',
})
export class ContactUsComponent extends BaseComponent {
  contactUsForm = new FormGroup({
    phone: new FormControl(''),
    address: new FormControl(''),

    whatsApp: new FormControl(''),
    facebook: new FormControl(''),
    instagram: new FormControl(''),
    twitter: new FormControl(''),
    snapchat: new FormControl(''),

    email: new FormControl('', Validators.compose([Validators.email])),
  });

  get phone(): any { return this.contactUsForm.get('phone'); }
  get email(): any { return this.contactUsForm.get('email'); }
  get address(): any { return this.contactUsForm.get('address'); }

  get whatsApp(): any { return this.contactUsForm.get('whatsApp'); }
  get facebook(): any { return this.contactUsForm.get('facebook'); }
  get instagram(): any { return this.contactUsForm.get('instagram'); }
  get twitter(): any { return this.contactUsForm.get('twitter'); }
  get snapchat(): any { return this.contactUsForm.get('snapchat'); }

  constructor() {
    super();
  }

  setResults(results) {
    this.phone.setValue(results.phone);
    this.email.setValue(results.email);
    this.address.setValue(results.address);

    this.whatsApp.setValue(results.whatsApp);
    this.facebook.setValue(results.facebook);
    this.instagram.setValue(results.instagram);
    this.twitter.setValue(results.twitter);
    this.snapchat.setValue(results.snapchat);
  }
}