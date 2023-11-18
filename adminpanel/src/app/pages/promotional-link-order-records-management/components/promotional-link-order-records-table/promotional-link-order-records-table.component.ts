import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { PackageService } from 'app/shared/services';
import { PromotionalLinkService } from 'app/shared/services/promotional-link';
import moment from 'moment';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'promotional-link-order-records-table',
  templateUrl: './promotional-link-order-records-table.component.html',
})
export class PromotionalLinkOrderRecordsTableComponent extends BaseComponent implements OnInit {

  // @ViewChild('viewModal') viewModal: any;
  // @ViewChild('deleteModal') deleteModal: any;

  selectedItem: any = null;
  promotionalLinkId;
  items = [];
  constructor(private fb: FormBuilder, private toastService: ToastrService, private _service: PromotionalLinkService, private changeDetector: ChangeDetectorRef, private router: ActivatedRoute) {
    super();
    this.promotionalLinkId = this.router.snapshot.params.id
  }

  form = this.fb.group({
    startDate: [moment().subtract(7, 'days'), Validators.required],
    endDate: [moment(), Validators.required]
  }) 

  ngOnInit(): void {
    this.get();
  }

  filterData(){
    if(this.form.valid) {
      const {startDate, endDate} = this.form.value
      this.getClientOrderReport(moment(startDate).format('MM-DD-YYYY'), moment(endDate).format('MM-DD-YYYY'))
    } else {
      this.toastService.error('Invalid form data')
    }
  }

  dateChange($event) {
    console.log($event)
  }

  get() {
    this.items = [];
    const startDate = moment().subtract(7, 'days').format('MM-DD-YYYY');
    const endDate = moment().format('MM-DD-YYYY')
    this.getClientOrderReport(startDate, endDate)
  }

  getClientOrderReport(startDate, endDate) {
    this._service.getClientOrderReportByPromotionalId(this.promotionalLinkId, startDate, endDate)
      .subscribe({
        next: (results: any) => {
          this.items = results.data;

        }, error: (error) => {
          this.showError(error);
        }
      });
  }

  // onSave(){
  //   this.viewModal.nativeElement.click();
  //   this.get()
  // }

  // onRowSelect(event) {
  //   this.selectedItem = event;
  // }

  // onDeleteRow() {
  //   this.deleteModal.nativeElement.click();
  // }

  // onEditRow(event) {
  //   console.log("Row updated")
  //   this.viewModal.nativeElement.click();
  // }

  // onAddRow() {
  //   console.log(this.viewModal.nativeElement)
  //   this.selectedItem = null;
  //   this.viewModal.nativeElement.click();
  //   this.changeDetector.detectChanges();
  // }

  viewRecords(id) {
    this.onTogglePage(`/promotional-link-order-records-management/${id}`)
  }

  // onConfirmDelete() {
  //   this._service.deletePromotionalLink(this.selectedItem.id).subscribe({
  //     next:() => {
  //       this.get()
  //     },
  //     error:(err) => {
  //       console.log(err)
  //     }
  //   })
  //   this.deleteModal.nativeElement.click();
  // }
}