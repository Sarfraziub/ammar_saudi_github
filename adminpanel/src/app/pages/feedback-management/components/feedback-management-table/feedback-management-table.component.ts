import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { StorageService } from 'app/shared/infrastructure/storage.service';
import { FeedbackService } from 'app/shared/services';
import moment from 'moment';
import Swal from 'sweetalert2';

@Component({
  selector: 'feedback-management-table-cmp',
  templateUrl: './feedback-management-table.html',
})
export class FeedbackManagementTableComponent extends BaseComponent implements OnInit {
  @ViewChild('changeStatusModal') changeStatusModal: any;
  @ViewChild('setDriverModal') setDriverModal: any;
  @ViewChild('deleteModal') deleteModal: any;
  @ViewChild('viewFeedbackModal') viewFeedbackModal: any;

  selectedItem: any = null;
  items = [];

  constructor(private _service: FeedbackService, private storageService: StorageService, private changeDetector: ChangeDetectorRef) {
    super();
  }

  ngOnInit(): void {
    this.get();
  }

  get() {
    this.items = [];
    this._service.get()
      .subscribe({
        next: (results) => {
          this.items = results.feedbacks;


          this.items.forEach(element => {
            element.createdFormat = moment.utc(element.created).local().format('DD/MM/YYYY hh:mm A');
            element.deliveryFormat = element.deliveryTime ? moment.utc(element.deliveryTime).local().format('DD/MM/YYYY hh:mm A') : 'لم يتم التسليم بعد';

          });
        }, error: (error) => {
          this.showError(error);
        }
      });
  }

  onRowSelect(event) {
    this.selectedItem = event;
    this.changeDetector.detectChanges();
  }

  onToggleLock(item, isShow) {
    Swal.fire({
      title: 'هل أنت متأكد؟',
      text: isShow ? `هل أنت متأكد من اغلاق الحساب` : `هل أنت متأكد من تفعيل الحساب`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      cancelButtonText: 'الغاء',
      confirmButtonText: 'تأكيد'
    }).then((result) => {
      if (result.isConfirmed) {
        this.onLockAction(item, isShow);
      }
    })
  }

  onLockAction(item, isShow = true) {

    let response;

    const data = {
      "clientOrderId": item.id,
      "hideFeedback": isShow
    }

    this._service.update(data).subscribe(() => {
      this.showSuccess('تمت العملية بنجاح');
      this.get();
    }, error => this.showError(error));
  }

  onViewDetails(event) {
    this.storageService.secureStorage.setItem('order', event);
    this.onTogglePage('order-details/' + event.id)
  }
}