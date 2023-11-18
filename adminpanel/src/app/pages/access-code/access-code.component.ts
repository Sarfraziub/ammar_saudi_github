import { Component, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { StorageService } from 'app/shared/infrastructure/storage.service';
import { AuthService, TokenStorageService } from 'app/shared/services';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'access-code-root',
    templateUrl: './access-code.component.html',
    encapsulation: ViewEncapsulation.None
})
export class AccessCodeComponent {

    isLoggedIn = false;
    isLoginFailed = false;
    errorMessage = '';
    roles: string[] = [];


    form = new FormGroup({
        phoneNumber: new FormControl('', Validators.required),
        token: new FormControl('', Validators.required),
        rememberMe: new FormControl(false)
    });

    get phoneNumber(): any { return this.form.get('phoneNumber'); }
    get token(): any { return this.form.get('token'); }
    get rememberMe(): any { return this.form.get('rememberMe'); }

    constructor(private storageService: StorageService,
        private toastrService: ToastrService, private authService: AuthService,
        private _tokenStorageService: TokenStorageService, private _router: Router) {

        const token = this._tokenStorageService.getToken();
        if (token) {
            this._router.navigate(["/pages"]);
        }

        const phoneNumber = this.storageService.secureStorage.getItem('phoneNumber');
        if (!phoneNumber) {
            this._router.navigate(["/login"]);
        }
        this.phoneNumber.setValue(phoneNumber);
    }

    onSubmit() {
        if (!this.form.valid) {
            return;
        }


        const data = {
            "phoneNumber": this.phoneNumber.value,
            "token": this.token.value,
            "deviceId": "string"
        }

        this.authService.login(data).subscribe(
            result => {
                const redirectUrl = this.storageService.secureStorage.getItem('redirect-url');
                this._tokenStorageService.saveToken(result);
                if (redirectUrl) {
                    this._router.navigate([redirectUrl]);
                } else {
                    this._router.navigate(["/pages"]);
                }
            },
            error => {
                this.toastrService.error(error.error);
            }
        );
    }
}
