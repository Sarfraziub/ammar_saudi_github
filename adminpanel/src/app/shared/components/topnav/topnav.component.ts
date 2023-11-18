import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { StorageService } from 'app/shared/infrastructure/storage.service';
import { ThemeService, TokenStorageService, UploadService } from 'app/shared/services';
import { Subscription } from 'rxjs';

@Component({
    selector: 'topnav-cmp',
    encapsulation: ViewEncapsulation.None,
    templateUrl: './topnav.component.html',
})

export class TopnavComponent extends BaseComponent implements OnInit {


    elem: any;
    enabledFullScreen: boolean = false;
    subscription: Subscription;
    imageUrl: string = 'assets/avatars/avatar-m.png';
    userInformation: any = {
        name: 'الاسم غير معرف',
        email: 'لا يوجد بريد الكتروني'
    };

    constructor(private _uploadService: UploadService, private tokenStorageService: TokenStorageService, private storageService: StorageService,
        @Inject(DOCUMENT) private document: any, private themeService: ThemeService) {
        super();
    }

    ngOnInit(): void {
        this.subscription = this._uploadService.onChangeUserInformation().subscribe(userInformation => {
            if (userInformation) {
                this.userInformation.name = userInformation?.name || this.userInformation.name;
                
                this.userInformation.email = userInformation?.email || this.userInformation.email;
            }
            this.imageUrl = userInformation.imageUrl || this.imageUrl;
        });
    }

    onToggleDocument(className) {
        document.body.classList.toggle(className);
    }

    openFullscreen() {
        var document: any = window.document;
        var Element: any = Element;
        if (!document.fullscreenElement && !document.mozFullScreenElement && !document.webkitFullscreenElement && !document.msFullscreenElement) {

            if (document.documentElement.requestFullscreen) {
                /* Standard browsers */
                document.documentElement.requestFullscreen();
            } else if (document.documentElement.msRequestFullscreen) {
                /* Internet Explorer */
                document.documentElement.msRequestFullscreen();
            } else if (document.documentElement.mozRequestFullScreen) {
                /* Firefox */
                document.documentElement.mozRequestFullScreen();
            } else if (document.documentElement.webkitRequestFullscreen) {
                /* Chrome */
                document.documentElement.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
            }

        } else {

            if (document.exitFullscreen) {
                document.exitFullscreen();
            } else if (document.msExitFullscreen) {
                document.msExitFullscreen();
            } else if (document.mozCancelFullScreen) {
                document.mozCancelFullScreen();
            } else if (document.webkitExitFullscreen) {
                document.webkitExitFullscreen();
            }
        }

    }

    /* Close fullscreen */
    closeFullscreen() {
        if (this.document.exitFullscreen) {
            this.document.exitFullscreen();
        } else if (this.document.mozCancelFullScreen) {
            /* Firefox */
            this.document.mozCancelFullScreen();
        } else if (this.document.webkitExitFullscreen) {
            /* Chrome, Safari and Opera */
            this.document.webkitExitFullscreen();
        } else if (this.document.msExitFullscreen) {
            /* IE/Edge */
            this.document.msExitFullscreen();
        }
    }

    printScreen() {
        window.print();
    }

    scrollTop() {
        window.scroll(0, 0);
    }

    logout(): void {
        this.tokenStorageService.signOut();
        window.location.reload();
    }

    ngOnDestroy() {
        this.subscription ? this.subscription.unsubscribe() : null;
    }

}
