import { Component, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { StorageService } from 'app/shared/infrastructure/storage.service';
import { AuthService, TokenStorageService } from 'app/shared/services';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'login-root',
    templateUrl: './login.component.html',
    encapsulation: ViewEncapsulation.None
})
export class LoginComponent {

    isLoggedIn = false;
    isLoginFailed = false;
    errorMessage = '';
    roles: string[] = [];


    form = new FormGroup({
        phoneNumber: new FormControl('', Validators.required),
    });

    get phoneNumber(): any { return this.form.get('phoneNumber'); }

    constructor(private storageService: StorageService,
        private toastrService: ToastrService, private authService: AuthService,
        private _tokenStorageService: TokenStorageService, private _router: Router) {
        const token = this._tokenStorageService.getToken();
        if (token) {
            this._router.navigate(["/pages"]);
        }

        this.storageService.secureStorage.removeItem('phoneNumber');
    }

    onSubmit() {

        if (!this.form.valid) {
            return;
        }

        const data = {
            "phoneNumber": this.phoneNumber.value,
        }
        this.authService.access(data).subscribe(
            result => {
                this.storageService.secureStorage.setItem('phoneNumber', this.phoneNumber.value.toString());
                this.toastrService.success(`تم ارسال رمز الدخول الى الرفم ${this.phoneNumber.value} بنجاح`);
                this._router.navigate(["/access-code"]);
            },
            error => {
                this.toastrService.error(error.error);
            }
        );
    }

    numberOnly(event): boolean {
        const charCode = (event.which) ? event.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
}
