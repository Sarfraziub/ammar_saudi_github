import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { Gallery } from 'app/shared/components';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { NotificationTemplateService, SliderItemService } from 'app/shared/services';
import Swal from 'sweetalert2';

@Component({
  selector: 'notification-template-table-cmp',
  templateUrl: './notification-template-table.html',
})
export class NotificationTemplateTableComponent extends BaseComponent implements OnInit {

  @ViewChild('viewModal') viewModal: any;
  @ViewChild('deleteModal') deleteModal: any;
  @ViewChild('closeSend') closeSend: any;

  selectedItem: any = null;
  items = [];
  constructor(private _service: NotificationTemplateService, private changeDetector: ChangeDetectorRef) {
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
          this.items = results.notificationTemplates;
        }, error: (error) => {
          this.showError(error);
        }
      });
  }

  onRowSelect(event) {
    this.selectedItem = event;
    this.changeDetector.detectChanges();
  }

  onDeleteRow() {
    this.deleteModal.nativeElement.click();
  }

  onEditRow(event) {
    this.viewModal.nativeElement.click();
  }

  onAddRow() {
    this.selectedItem = null;
    this.changeDetector.detectChanges();
    this.viewModal.nativeElement.click();
  }


  onConfirmDelete() {
    this._service.delete(this.selectedItem.id).subscribe(() => {
      this.showSuccess('تمت العملية بنجاح');
      this.get();
    }, error => this.showError(error));
    this.deleteModal.nativeElement.click();
  }

  onSend(item) {
    Swal.fire({
      title: 'هل أنت متأكد؟',
      text: `هل أنت متأكد من ارسال الاشعار ${item.title}`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      cancelButtonText: 'الغاء',
      confirmButtonText: 'تأكيد'
    }).then((result) => {
      if (result.isConfirmed) {
        this.onConfirmSend(item);
      }
    })
  }

  onConfirmSend(item) {
    const data = {
      id: this.selectedItem.id
    }
    this._service.send(data).subscribe(() => {
      this.showSuccess('تمت العملية بنجاح');
    }, error => this.showError(error));
  }

}