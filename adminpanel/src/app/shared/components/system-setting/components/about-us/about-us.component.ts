import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';

@Component({
  selector: 'about-us-cmp',
  templateUrl: './about-us.html',
})
export class AboutUsComponent extends BaseComponent {
  aboutUsForm = new FormGroup({
    content: new FormControl(''),
    arabicContent: new FormControl(''),
  });

  get content(): any { return this.aboutUsForm.get('content'); }
  get arabicContent(): any { return this.aboutUsForm.get('arabicContent'); }

  constructor() {
    super();
  }

  setResults(results) {
    this.content.setValue(results.content);
    this.arabicContent.setValue(results.arabicContent);
  }
}