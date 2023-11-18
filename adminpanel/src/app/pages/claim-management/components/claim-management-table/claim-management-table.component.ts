import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { StorageService } from 'app/shared/infrastructure/storage.service';
import { ClaimService } from 'app/shared/services';
import moment from 'moment';

@Component({
  selector: 'claim-management-table-cmp',
  templateUrl: './claim-management-table.html',
})
export class ClaimManagementTableComponent extends BaseComponent implements OnInit {

  @ViewChild('downloadFile') downloadFile: any;


  selectedItem: any = null;
  items = [];
  srcFile: any = null;
  constructor(private storageService: StorageService, private _service: ClaimService, private changeDetector: ChangeDetectorRef) {
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
          this.items = results.claims;
          this.items.forEach(element => {
            element.createdFormat = moment.utc(element.created).local().format('DD/MM/YYYY hh:mm A');;
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

  onEditRow(event) {
    this.selectedItem = event;
    
    const a = document.createElement('a');
    a.setAttribute('href', event.receiptUrl);
    a.setAttribute('Claim', 'claim');
    a.click();

    // release the object url since it's not needed
    // though browsers release object urls automatically,it's for optimal performance
    // and memory usage to explicitly release it
    window.URL.revokeObjectURL(event.receiptUrl);
  }

  onViewDetails(event) {
    this.storageService.secureStorage.setItem('claim', event);
    this.onTogglePage('claim-details/' + event.id)
  }
}