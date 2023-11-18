import { Injector, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { routing } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { PagesModule } from './pages/pages.module';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { LoginComponent } from './pages/login';
import { AccessCodeComponent } from './pages/access-code';
import { PrivacyPolicyComponent } from './pages/privacy-policy';

declare module "@angular/core" {
  interface ModuleWithProviders<T = any> {
    ngModule: Type<T>;
    providers?: Provider[];
  }
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AccessCodeComponent,
    PrivacyPolicyComponent
  ],
  imports: [
    HttpClientModule,
    FormsModule, ReactiveFormsModule,
    CommonModule,
    BrowserModule,
    NgbModule,
    routing,
    PagesModule,
    BrowserAnimationsModule,
    SharedModule.forRoot(),
    ToastrModule.forRoot(
      {
        timeOut: 10000,
        positionClass: 'toast-top-right',
        preventDuplicates: true,
        closeButton: false
      }
    ),
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule {

  static injector: Injector;

  constructor(injector: Injector) {
    AppModule.injector = injector;
  }
}
