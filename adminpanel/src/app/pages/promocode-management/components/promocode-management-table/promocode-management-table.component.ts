import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { PromocodeService } from 'app/shared/services';
import moment from 'moment';

@Component({
  selector: 'promocode-management-table-cmp',
  templateUrl: './promocode-management-table.html',
})
export class PromoCodeManagementTableComponent extends BaseComponent implements OnInit {

  @ViewChild('viewModal') viewModal: any;
  @ViewChild('deleteModal') deleteModal: any;

  selectedItem: any = null;
  items = [];
  constructor(private _service: PromocodeService, private changeDetector: ChangeDetectorRef) {
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
          this.items = results.promoCodes;
          this.items.forEach(element => {
            element.name = element.code;
            element.expiryDateFormat = moment.utc(element.expiry).local().format('DD/MM/YYYY');;
          })
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
    if (!this.selectedItem.allowUpdate) {
      this.showError('الرمز الترويجي مستخدم مسبقا لا يمكن التعديل عليه');
      return;
    }
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
}