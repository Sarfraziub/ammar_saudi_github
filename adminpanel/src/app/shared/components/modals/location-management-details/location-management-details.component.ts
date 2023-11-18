import { Component, ElementRef, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { LocationTypeLookup } from 'app/shared/lookup';
import { LocationService, RegionService, UploadService } from 'app/shared/services';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { ImageViewer } from 'app/shared/components';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'location-management-details-cmp',
  templateUrl: './location-management-details.html',
})
export class LocationManagementDetailsComponent extends BaseComponent implements OnChanges {

  @ViewChild('imageViewer') imageViewer: ImageViewer;
  @ViewChild('cancelAction') cancelAction: any;

  marker: any = null;
  @Output() onSave = new EventEmitter<boolean>();
  private _selectedItem: any = null;
  @Input() get selectedItem() {
    return this._selectedItem;
  }
  @Input() isEdit?: boolean;

  set selectedItem(item: any) {
    if (item) {
      this.getById(item);
    } else {
      this.onReset(this.form);
      this.marker = null;

      if (this.imageViewer) {
        this.imageViewer.attachments = [];
      }
    }
  }

  form = new FormGroup({

    id: new FormControl(null),
    region: new FormControl(''),
    regionId: new FormControl(null, Validators.required),
    name: new FormControl('', Validators.required),
    arabicName: new FormControl('', Validators.required),
    longitude: new FormControl('', Validators.required),
    latitude: new FormControl('', Validators.required),
    locationType: new FormControl(null, Validators.required),
    districtArabicName: new FormControl(null),
    districtName: new FormControl(null),
    description: new FormControl(''),
    arabicDescription: new FormControl(''),
    moreNeeded: new FormControl(false),
    locationTypeTitle: new FormControl(''),
    active: new FormControl(null),
    imageUrls: new FormControl(''),
    url: new FormControl('')
  });

  get id(): any { return this.form.get('id'); }
  get region(): any { return this.form.get('region'); }
  get regionId(): any { return this.form.get('regionId'); }
  get name(): any { return this.form.get('name'); }
  get arabicName(): any { return this.form.get('arabicName'); }
  get longitude(): any { return this.form.get('longitude'); }
  get latitude(): any { return this.form.get('latitude'); }
  get locationType(): any { return this.form.get('locationType'); }
  get description(): any { return this.form.get('description'); }
  get arabicDescription(): any { return this.form.get('arabicDescription'); }
  get moreNeeded(): any { return this.form.get('moreNeeded'); }
  get url(): any { return this.form.get('url'); }
  get districtName(): any { return this.form.get('districtName'); }
  get districtArabicName(): any { return this.form.get('districtArabicName'); }
  get active(): any { return this.form.get('active'); }

  locationTypes: any[] = [];
  regions: any[] = [];

  constructor(private _service: LocationService, 
    private _uploadService: UploadService, 
    private _regionService: RegionService,
    private toastr: ToastrService,
    private elementRef: ElementRef
  ) {
    super();
    this.locationTypes = LocationTypeLookup.getLookup;
    this.regions = [];
  }

  ngOnInit(): void {
    this.getRegions();
  }

  getById(item) {
    this.imageViewer.attachments = [];
    this._service.getById(item.id)
      .subscribe({
        next: (result) => {
          this.form.setValue({ ...this.form.value, ...result });
          this.marker = {
            lat: this.latitude.value,
            lng: this.longitude.value
          };
          if (result.imageUrls.length > 0) {
            result.imageUrls.forEach(element => {
              this.imageViewer.attachments.push({
                file: element.url,
                id: element.id,
              })
            });
          }
        }, error: (error) => {
          this.showError('Location not found');
         this.elementRef.nativeElement.querySelector('.close').click()
        }
      });
  }

  getRegions() {
    this._regionService.get()
      .subscribe({
        next: (results) => {
          this.regions = results.regions.map(m => ({
            text: m.arabicName,
            id: m.id
          }));
        }, error: (error) => {
          this.showError(error);
        }
      });
  }

  onSetMarker(event) {
    this.latitude.setValue(event.event.latLng.lat());
    this.longitude.setValue(event.event.latLng.lng());

    this.url.setValue(event.address[0].formatted_address);
  }

  onSubmit() {
    this.form.markAllAsTouched();
    if (!this.form.valid) {
      this.toastr.error("Please fill fields")
      this.validateAllFormFields(this.form);
      return;
    }

    let data = this.setData(this.form);
    data.locationType = +data.locationType;
    data.regionId = +data.regionId;
    data.moreNeeded = data.moreNeeded || false;

    if (this.id.value) {
      this.onUpdate(data);
    } else {
      this.onAdd(data);
    }
  }

  onAdd(data) {
    let images: any[] = [];
    if (this.imageViewer.attachments.length > 0) {
      this.imageViewer.attachments.forEach(element => {
        this._uploadService.upload(element.src).subscribe(
          (data) => {
            if (data) {
              images.push(data);
              if (images.length === this.imageViewer.attachments.length) {
                data.images = images;
                this.submit(data);
              }
            }
          },
          error => {
            this.showError(error);
          });
      });
    } else {
      this.submit(data);
    }
  }

  onUpdate(data) {
    let images: any[] = [];
    const attachments = this.imageViewer.attachments?.filter(f => !f.id);
    if (attachments.length > 0) {
      attachments.forEach(element => {
        this._uploadService.upload(element.src).subscribe(
          (result) => {
            if (result) {
              images.push(result);
              if (images.length === attachments.length) {
                this.addImages(images, data);
              }
            }
          },
          error => {
            this.showError(error);
          });
      });

    } else {
      this.submit(data);
    }
  }

  addImages(images, data) {
    images.forEach((element, index) => {
      const item = {
        "imageId": element,
        "locationId": this.id.value
      }
      this._service.addImage(item).subscribe(() => {
        if (index + 1 === images.length) {
          this.submit(data);
        }
      }, error => this.showError(error));
    });
  }

  submit(data) {
    let response;
    if (this.id.value) {
      response = this._service.update(data);
    } else {
      response = this._service.add(data);
    }

    response.subscribe(() => {
      this.onReset(this.form);
      this.marker = null;
      this.onComplete(this.cancelAction, this.onSave);
    }, error => this.showError(error));
  }

  onRemoveImage(event) {
    if (event.id) {
      this._service.removeImage(event.id).subscribe(() => {
        this.imageViewer.removeItem(event);
      }, error => this.showError(error));
    } else {
      this.imageViewer.removeItem(event);
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes?.isEdit?.currentValue) {
      this.form.controls.active.setValidators([Validators.required]);
    } else {
      this.form.controls.active.removeValidators(Validators.required)
    }
    this.form.controls.active.updateValueAndValidity();
  }

  setIsActive(event) {
    this.form.patchValue({
      active: event.target.checked ? 1 : 0
    })
  }
}