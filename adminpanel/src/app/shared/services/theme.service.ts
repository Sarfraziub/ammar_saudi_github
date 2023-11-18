import { Injectable } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Injectable()
export class ThemeService {

  constructor(public sanitizer: DomSanitizer) {

  }

  getThemeUrl(theme) {
    if (theme) {
      return this.sanitizer.bypassSecurityTrustResourceUrl(`${theme}.css`);
    } else {
      return null;
    }
  }
}
