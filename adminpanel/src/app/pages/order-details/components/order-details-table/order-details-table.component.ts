import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { StorageService } from 'app/shared/infrastructure/storage.service';
import { ClientOrderService } from 'app/shared/services';

@Component({
  selector: 'order-details-table-cmp',
  templateUrl: './order-details-table.html',
})
export class OrderDetailsTableComponent extends BaseComponent implements OnInit {

  @ViewChild('changeStatusModal') changeStatusModal: any;
  @ViewChild('setDriverModal') setDriverModal: any;
  @ViewChild('deleteModal') deleteModal: any;
  @ViewChild('viewFeedbackModal') viewFeedbackModal: any;

  selectedItem: any = null;
  id: number;
  order: any;
  results: any = null;
  total: number = 0;
  dicount: number = 0;
  constructor(private storageService: StorageService, public route: ActivatedRoute, private _service: ClientOrderService, private changeDetector: ChangeDetectorRef) {
    super();
    const id = this.route.snapshot.params['id'];
    const order = this.storageService.secureStorage.getItem('order');
    if (!id || !order) {
      this.onTogglePage('');
    }

    this.id = id;
    this.order = order;
  }

  ngOnInit(): void {
    this.get();
  }

  get() {
    this._service.get(this.id)
      .subscribe({
        next: (results) => {
          this.results = results;
          this.total = results.clientOrderDetails
            .reduce((sum, current) => sum + (current.saleItemPrice * current.saleItemQuantity), 0);

        }, error: (error) => {
          this.showError(error);
        }
      });
  }

  onRowSelect(event) {
    this.selectedItem = event;
    this.changeDetector.detectChanges();
  }

  onCancelOrder(event) {
    this.selectedItem = event;
    this.deleteModal.nativeElement.click();
  }

  onchangeStatus(event) {
    this.selectedItem = event;
    this.changeStatusModal.nativeElement.click();
  }

  onSetDriver(event) {
    this.selectedItem = event;
    this.setDriverModal.nativeElement.click();
  }

  onViewFeedback(event) {
    this.selectedItem = event;
    this.viewFeedbackModal.nativeElement.click();
  }

  onConfirmDelete() {
    this.get();
    this.deleteModal.nativeElement.click();
  }
}