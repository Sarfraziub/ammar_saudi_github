import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { StorageService } from 'app/shared/infrastructure/storage.service';
import { ClaimService } from 'app/shared/services';

@Component({
  selector: 'claim-details-table-cmp',
  templateUrl: './claim-details-table.html',
})
export class ClaimDetailsTableComponent extends BaseComponent implements OnInit {

  @ViewChild('changeStatusModal') changeStatusModal: any;
  @ViewChild('setDriverModal') setDriverModal: any;
  @ViewChild('deleteModal') deleteModal: any;
  @ViewChild('viewFeedbackModal') viewFeedbackModal: any;

  selectedItem: any = null;
  id: number;
  claim: any;
  results: any = null;
  total: number = 0;
  dicount: number = 0;
  constructor(private storageService: StorageService, public route: ActivatedRoute, private _service: ClaimService, private changeDetector: ChangeDetectorRef) {
    super();
    const id = this.route.snapshot.params['id'];
    const claim = this.storageService.secureStorage.getItem('claim');
    if (!id || !claim) {
      this.onTogglePage('claim-management');
    }

    this.id = id;
    this.claim = claim;
  }

  ngOnInit(): void {
    this.get();
  }

  get() {
    this._service.getClientOrdersByDriverClaimId(this.id)
      .subscribe({
        next: (results) => {
          this.results = results;
          this.total = results.clientOrders
            .reduce((sum, current) => sum + current.fee, 0);

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