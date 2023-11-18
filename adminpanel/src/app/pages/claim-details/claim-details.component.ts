import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { StorageService } from 'app/shared/infrastructure/storage.service';
import { ClaimService, UploadService } from 'app/shared/services';
import Swal from 'sweetalert2';

@Component({
    selector: 'claim-details',
    templateUrl: './claim-details.html',
})
export class ClaimDetailsComponent extends BaseComponent {
    isFullScreen: boolean;
    @ViewChild('uploadFile') uploadFile: any;
    id: number = null;
    claim: any;

    constructor(private storageService: StorageService, private _uploadService: UploadService, private _service: ClaimService, public route: ActivatedRoute) {
        super();
        const id = this.route.snapshot.params['id'];
        const claim = this.storageService.secureStorage.getItem('claim');
        if (!id || !claim) {
            this.onTogglePage('claim-management');
        }
        this.id = id;
        this.claim = claim;


    }

    onToggleUpload() {
        this.uploadFile.nativeElement.click();
    }

    attachments = [];
    selectedFile = null;

    onSelect(event) {

        this.attachments = [];
        this.selectedFile = event.target.files[0];
        if (event.target.files && event.target.files[0]) {
            var filesAmount = event.target.files.length;

            for (let i = 0; i < filesAmount; i++) {
                if (this.attachments.find(f => f.src.name === event.target.files[i].name)) {
                } else {
                    const reader = new FileReader();
                    reader.onload = (e: any) => {
                        this.attachments.push({ file: e.target.result, src: event.target.files[i] });
                    };

                    reader.readAsDataURL(event.target.files[i]);
                }
            }
        }
    }

    onRemove(item) {
        this.attachments = this.attachments.filter(f => f !== item);
    }



    onCloseClaim() {
        Swal.fire({
            title: 'هل أنت متأكد؟',
            text: `هل أنت متأكد من اغلاق الطلب`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'الغاء',
            confirmButtonText: 'تأكيد'
        }).then((result) => {
            if (result.isConfirmed) {
                this.onSubmit();
            }
        })
    }

    onSubmit() {

        let receiptId = null;
        this._uploadService.upload(this.selectedFile).subscribe(
            (id) => {
                const data = {
                    "id": this.id,
                    "receiptId": id
                }

                this._service.closeClaim(data).subscribe(() => {
                    this.showSuccess('تمت العملية بنجاح');
                    this.onTogglePage('claim-management');
                }, error => this.showError(error));
            },
            error => {
                this.showError(error);
            });


    }
}
