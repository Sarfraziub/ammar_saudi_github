import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { LocationTypeLookup } from 'app/shared/lookup';
import { LocationService } from 'app/shared/services';

@Component({
  selector: 'location-management-table-cmp',
  templateUrl: './location-management-table.html',
})
export class LocationManagementTableComponent extends BaseComponent implements OnInit {

  @ViewChild('viewModal') viewModal: any;
  @ViewChild('deleteModal') deleteModal: any;

  selectedItem: any = null;
  items = [];
  isEdit = false;
  constructor(private _service: LocationService, private changeDetector: ChangeDetectorRef) {
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
          this.items = results.locations;
          this.items.forEach(element =>{
            element.locationTypeTitle = LocationTypeLookup.getById(element.locationType);
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
    this.isEdit = true;
    this.viewModal.nativeElement.click();
  }

  onAddRow() {
    this.isEdit = false;
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