import { Component, OnInit, Renderer2 } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { StorageService } from 'app/shared/infrastructure/storage.service';
import { UploadService } from 'app/shared/services';
import { Subscription } from 'rxjs';

@Component({
    selector: 'sidebar-cmp',
    templateUrl: './sidebar.component.html',
})
export class SidebarComponent extends BaseComponent implements OnInit {

    menus: any[] = [];
    isFilter: boolean;
    viewUser: boolean;
    viewLocation: boolean;
    viewItem: boolean;
    imageUrl: string = 'assets/avatars/avatar-m.png';
    subscription: Subscription;
    userInformation: any = {
        name: 'الاسم غير معرف',
        email: 'لا يوجد بريد الكتروني'
    };
    constructor(private storageService: StorageService, private _uploadService: UploadService, private renderer: Renderer2) {
        super();
        this.renderer.listen('window', 'click', (e: any) => {
            if (e.target.classList.contains('page-content-overlay')) {
                document.body.className = document.body.className.replace("mobile-nav-on", "");
                this.storageService.secureStorage.setItem('bodyClass', document.body.classList.toString());
            }
        });
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

    onToggleUser() {
        this.viewLocation = false;
        this.viewItem = false;
        this.viewUser = !this.viewUser;
    }

    onToggleLocation() {
        this.viewUser = false;
        this.viewItem = false;
        this.viewLocation = !this.viewLocation;
    }

    onToggleItem() {
        this.viewUser = false;
        this.viewLocation = false;
        this.viewItem = !this.viewItem;
    }

    onToggleFilter() {
        this.isFilter = !this.isFilter;
    }


}
