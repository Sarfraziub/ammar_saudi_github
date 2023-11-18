import { FormGroup, FormControl } from '@angular/forms';
// import { TokenStorageService } from '../services';
import { AppModule } from '../../app.module';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

import { CalendarType } from '../enum/calendar-type';
import { TokenStorageService } from '../services';

export class BaseComponent {

    calendarType = CalendarType;
    user: any = null;
    private toastrService: ToastrService;

    private _tokenStorageService: TokenStorageService;
    public _router;
    token: any = null;
    decodedToken: any = null;
    options: any = null;

    constructor() {
        // Access injector defined in AppModule to access singleton services
        this._router = AppModule.injector.get(Router);
        this._tokenStorageService = AppModule.injector.get(TokenStorageService);
        this.toastrService = AppModule.injector.get(ToastrService);
        this.options = {
            "language": {
                "noResults": function () {
                    return "لا يوجد بيانات";
                }
            },
            escapeMarkup: function (markup) {
                return markup;
            }
        }
        this.token = this._tokenStorageService.getToken();
        if (this.token) {
            this.decodedToken = this._tokenStorageService.deocodeToken(this.token.accessToken);
            // this.handleRefreshToken(this.token)
        } else {
            this._tokenStorageService.tokenSignOut();
            this._router.navigate(['./login']);
        }

    }

    validateAllFormFields(formGroup: FormGroup) {
        Object.keys(formGroup.controls).forEach(field => {
            const control = formGroup.get(field);
            if (control instanceof FormControl && control.enabled) {
                control.markAsTouched({ onlySelf: true });
            } else if (control instanceof FormGroup) {
                this.validateAllFormFields(control);
            }
        });
    }

    resetValue(formGroup: FormGroup) {
        Object.keys(formGroup.controls).forEach(field => {
            const control = formGroup.get(field);
            if (control instanceof FormControl && control.enabled) {
                control.setValue(null);
            } else if (control instanceof FormGroup) {
                this.resetValue(control);
            }
        });
    }
    showError(error) {
        if (error?.error && error?.error.length > 0) {
            if (error?.error[0] && error?.error[0]) {
                if (Array.isArray(error.error)) {
                    error.error?.forEach(element => {
                        element.forEach(item => {
                            this.toastrService.error(item.errorMessage);
                        })
                    }); return;
                }
            }
        }

        if (error?.error) {
            this.toastrService.error(error?.error);
            return;
        }

        this.toastrService.error(error.statusText ? error.statusText : error.message ? error.message : error);
    }

    showSuccess(message: string) {
        this.toastrService.success(message);
    }

    showInformation(message: string) {
        this.toastrService.info(message);
    }

    showWarning(message: string) {
        this.toastrService.warning(message);
    }

    numberOnly(event): boolean {
        const charCode = (event.which) ? event.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }

    isArabic(event) {
        const pattern = /[\u0600-\u06FF\u0750-\u077F]/;
        const result = pattern.test(event.key);
        return result;
    }

    englishOnly(event) {
        var regex = /^[a-zA-Z\s]|[1-9]+$/;
        if (regex.test(event.key)) {
            return true;
        } else {
            if (event.key === '') {
                return true;
            }
        }
        event.preventDefault();
        return false;

    }

    onTogglePage(page) {
        this._router.navigate([`/pages/${page}`]);
    }

    toISOLocal(date) {
        let z = n => ('0' + n).slice(-2);
        let zz = n => ('00' + n).slice(-3);
        let off = date.getTimezoneOffset();
        let sign = off > 0 ? '-' : '+';
        off = Math.abs(off);

        return date.getFullYear() + '-'
            + z(date.getMonth() + 1) + '-' +
            z(date.getDate()) + 'T' +
            z(date.getHours()) + ':' +
            z(date.getMinutes()) + ':' +
            z(date.getSeconds()) + '.' +
            zz(date.getMilliseconds()) +
            sign + z(off / 60 | 0) + ':' + z(off % 60);
    }

    onToggleFullScreen(item) {
        document.body.classList.toggle('panel-fullscreen');
        return !item;

    }

    isEmptyOrSpaces(str) {
        return str === null || str.match(/^ *$/) !== null;
    }

    completeRequest(message, onSave) {

        this.showSuccess(message);

        onSave?.emit(true)
    }


    onReset(form) {
        this.resetValue(form);
        form.reset();
    }

    setData(form) {
        return { ...{}, ...form.value };
    }

    onComplete(cancelAction, onSave) {
        cancelAction?.nativeElement.click();
        this.showSuccess("تمت عملية الحفظ بنجاح");
        onSave.emit();
    }
}