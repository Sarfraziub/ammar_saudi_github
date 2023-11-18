import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { PromotionalLinkService } from 'app/shared/services/promotional-link';

@Component({
  selector: 'promotional-link-table',
  templateUrl: './promotional-link-table.component.html',
})
export class PromotionalLinkTableComponent extends BaseComponent implements OnInit {

  @ViewChild('viewModal') viewModal: any;
  @ViewChild('deleteModal') deleteModal: any;

  selectedItem: any = null;
  items = [];
  constructor(private _service: PromotionalLinkService, private changeDetector: ChangeDetectorRef, private router: Router) {
    super();
  }

  ngOnInit(): void {
    this.get();
  }

  get() {
    this.items = [];
    this._service.getAllPromotionalLink()
      .subscribe({
        next: (results: any) => {
          this.items = results.data;

        }, error: (error) => {
          this.showError(error);
        }
      });
  }

  onSave(){
    this.viewModal.nativeElement.click();
    this.get()
  }

  onRowSelect(event) {
    this.selectedItem = event;
  }

  onDeleteRow() {
    this.deleteModal.nativeElement.click();
  }

  onEditRow(event) {
    console.log("Row updated")
    this.viewModal.nativeElement.click();
  }

  onAddRow() {
    console.log(this.viewModal.nativeElement)
    this.selectedItem = null;
    this.viewModal.nativeElement.click();
    this.changeDetector.detectChanges();
  }

  viewRecords(id) {
    this.onTogglePage(`/promotional-link-order-records-management/${id}`)
  }

  onConfirmDelete() {
    this._service.deletePromotionalLink(this.selectedItem.id).subscribe({
      next:() => {
        this.get()
      },
      error:(err) => {
        console.log(err)
      }
    })
    this.deleteModal.nativeElement.click();
  }
}