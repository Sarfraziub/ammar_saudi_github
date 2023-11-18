import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppModule } from '../app.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TokenInterceptor } from 'app/shared/infrastructure/token.interceptor';


import {
    SidebarComponent,
    TopnavComponent,
    FooterComponent,
    BreadcrumbComponent,
    DeleteModalComponent,
    SystemSettingComponent,
    MainSettingsComponent,
    UserProfileComponent,
    ContactUsComponent,
    AboutUsComponent
} from './components';

const NGA_PIPES = [
    ProfilePicturePipe,
    BaDomSanitizerPipe,
    ColumnVisibilityPipe,
];

import {
    BaseComponent,
} from './infrastructure/base.component';

import {

    AuthGuard,
    AuthService,
    TokenStorageService,
    LoadingService,
    ThemeService,

} from './services';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {
    DatePickerModule, TimePickerModule, TreeViewComponent,
    ImageViewer, UploadFile, GalleryModule,
    StartRateModule, SelectModule,
    DataTableModule, AppGoogleMapModule
} from './components/controls';
import { RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ProfilePicturePipe, BaDomSanitizerPipe, ColumnVisibilityPipe } from './pipes';
import { Services } from './business.services';


const NGA_SERVICES = [
    AuthGuard,
    AuthService,
    TokenStorageService,
    LoadingService,
    ThemeService,
    {
        provide: HTTP_INTERCEPTORS,
        useClass: TokenInterceptor,
        multi: true
    }
]

const NGA_COMPONENTS = [
    SidebarComponent,
    TopnavComponent,
    FooterComponent,
    BreadcrumbComponent,
    TreeViewComponent,
    ImageViewer,
    UploadFile,
    DeleteModalComponent,
    SystemSettingComponent,
    MainSettingsComponent,
    ContactUsComponent,
    UserProfileComponent,
    AboutUsComponent
];

const NGA_INFRASTRUCTURE = [
    BaseComponent,
];

@NgModule({
    declarations: [
        ...NGA_PIPES,
        ...NGA_COMPONENTS,
    ],
    imports: [
        RouterModule,
        CommonModule,
        FormsModule,
        NgbModule,
        ReactiveFormsModule,
        DatePickerModule,
        TimePickerModule,
        GalleryModule,
        StartRateModule,
        SelectModule,
        DataTableModule,
        AppGoogleMapModule
    ],
    exports: [
        ...NGA_PIPES,
        ...NGA_COMPONENTS,
        DataTableModule,
        DatePickerModule,
        TimePickerModule,
        GalleryModule,
        SelectModule,
        StartRateModule,
        AppGoogleMapModule
    ]
})



export class SharedModule {
    static forRoot(): ModuleWithProviders<AppModule> {
        return {
            ngModule: SharedModule,
            providers: [
                ...NGA_SERVICES,
                ...NGA_INFRASTRUCTURE,
                ...Services,
            ],
        };
    }
}