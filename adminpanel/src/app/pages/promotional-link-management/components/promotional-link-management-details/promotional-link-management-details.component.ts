import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { DomSanitizer } from "@angular/platform-browser";
import { BaseComponent } from "app/shared/infrastructure/base.component";
import { PromotionalLinkService } from "app/shared/services/promotional-link";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "promotional-link-management-details",
  templateUrl: "./promotional-link-management-details.component.html",
})
export class PromotionalLinkDetailsComponent extends BaseComponent implements OnInit {
  @ViewChild("cancelAction") cancelAction: any;
  isItemUploaded: boolean;
  itemImage: any = null;
  @Output() onSave = new EventEmitter<boolean>();
  private _selectedItem: any = null;
  isUniqueNameExist: boolean = false;
  @Input() get selectedItem() {
    return this._selectedItem;
  }

  set selectedItem(item: any) {
    this.isItemUploaded = false;
    this.itemImage = null;
    if (item) {
      this.form.setValue({ ...this.form.value, ...item });
      this.itemImage = item.imageUrl || this.itemImage;
    } else {
      this.onReset(this.form);
    }
  }

  form = new FormGroup({
    id: new FormControl(null),
    name: new FormControl("", Validators.required),
    email: new FormControl("", [Validators.required, Validators.email]),
    mobileNo: new FormControl("", Validators.required),
    uniqueName: new FormControl("", Validators.required),
  });

  get id() {
    return this.form.get("id");
  }
  get name() {
    return this.form.get("name");
  }
  get email() {
    return this.form.get("email");
  }
  get mobile() {
    return this.form.get("mobileNo");
  }
  get uniqueName() {
    return this.form.get("uniqueName");
  }

  constructor(private _sanitizer: DomSanitizer, private _service: PromotionalLinkService, private toastService: ToastrService) {
    super();
  }

  ngOnInit(): void {
    console.log(this.selectedItem)
  }

  onSubmit() {
    console.log(this.selectedItem, this.form.value);
    if (!this.form.valid || this.isUniqueNameExist) {
      this.validateAllFormFields(this.form);
      return;
    }
    const {value} = this.form
    if (value.id) {
      const body = {
        name: value.name,
        uniqueName: value.uniqueName,
        Id: value.id,
        email: value.email,
        mobileNo: value.mobileNo
      }
      this._service.updatePromotionalLink(body).subscribe({
        next: (response) => {
          this.toastService.success("Promotional Link Updated", "Success")
          this.form.reset();
          this.onSave.emit();
        },
        error: (error) => {
          this.toastService.error("Error")
        }
      })
    } else {
      this._service.addPromotionalLink(value).subscribe({
        next: (response) => {
          this.toastService.success("Promotional Link Added", "Success")
          this.form.reset();
          this.onSave.emit();
        },
        error: (error) => {
          this.toastService.error("Error",)
        }
      })
    }
  

  }

  public uniqueNameAvailability(event) {
    const { value } = event.target;
    if (value.length > 1) {
      this._service.checkUniqueNameAvailability(value).subscribe({
        next: (response: boolean) => {
          this.isUniqueNameExist = response
        },
        error: (err) => console.log(err),
      });
    }
  }
}
