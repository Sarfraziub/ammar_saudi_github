import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { AdminService, ClientService } from 'app/shared/services';
import Swal from 'sweetalert2';

@Component({
  selector: 'user-management-table-cmp',
  templateUrl: './user-management-table.html',
})
export class UserManagementTableComponent extends BaseComponent implements OnInit {

  selectedItem: any = null;
  items = [];
  constructor(private _adminService: AdminService, private _service: ClientService, private changeDetector: ChangeDetectorRef) {
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
          this.items = results.clients;
        }, error: (error) => {
          this.showError(error);
        }
      });
  }

  onRowSelect(event) {
    this.selectedItem = event;
    this.changeDetector.detectChanges();
  }

  
  onToggleLock(item, isLock) {
    Swal.fire({
      title: 'هل أنت متأكد؟',
      text: isLock ? `هل أنت متأكد من اغلاق الحساب` : `هل أنت متأكد من تفعيل الحساب`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      cancelButtonText: 'الغاء',
      confirmButtonText: 'تأكيد'
    }).then((result) => {
      if (result.isConfirmed) {
        this.onLockAction(item, isLock);
      }
    })
  }

  onLockAction(item, isLock = true) {

    let response;

    const data = {
      "id": item.clientId
    }
    if (isLock) {
      response = this._adminService.lockoutAccount(data);
    } else {
      response = this._adminService.unlockedAccount(data);
    }

    response.subscribe(() => {
      this.showSuccess('تمت العملية بنجاح');
      this.get();
    }, error => this.showError(error));
  }

}