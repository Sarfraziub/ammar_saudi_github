import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { ManageClientOrderService } from 'app/shared/services';
import moment from 'moment';
import 'moment-timezone';
import { ActivatedRoute } from '@angular/router';
import { StorageService } from 'app/shared/infrastructure/storage.service';
import { ClientOrderStatus } from 'app/shared/enum';
import { Gallery } from 'app/shared/components';
import Swal from 'sweetalert2';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { OrderManagementLocationComponent } from '../order-management-location/order-management-location.component';
@Component({
  selector: 'order-management-table-cmp',
  templateUrl: './order-management-table.html',
})
export class OrderManagementTableComponent extends BaseComponent implements OnInit {

  @ViewChild('setDriverModal') setDriverModal: any;
  @ViewChild('setLocationModal') setLocationModal: any;
  @ViewChild('deleteModal') deleteModal: any;
  @ViewChild('viewFeedbackModal') viewFeedbackModal: any;
  @ViewChild('gallery') gallery: Gallery;

  selectedItem: any = null;
  items = [];
  originalItems = [];
  clientId: number;
  filterType: number = 1;
  viewGallery: boolean;

  clientOrderStatus: typeof ClientOrderStatus = ClientOrderStatus;
  constructor(private storageService: StorageService,
    public route: ActivatedRoute,
    private _service: ManageClientOrderService, 
    private changeDetector: ChangeDetectorRef,
    private ngModal: NgbModal
  ) {
    super();
    const clientId = this.route.snapshot.params['id'];

    this.clientId = clientId ?? null;
  }

  ngOnInit(): void {
    this.get();
  }

  get() {
    this.items = [];
    this.originalItems = [];
    let response;

    if (this.clientId) {
      response = this._service.getClientOrdersByClientId(this.clientId);
    } else {
      response = this._service.viewClientOrders();
    }

    response.subscribe({
      next: (results) => {
        const localtz = moment.tz.guess()
        results.clientOrders.forEach(element => {
          element.createdFormat = moment.utc(element.created).local().format('DD/MM/YYYY hh:mm A');
          element.updatedFormat = moment.utc(element.updated).local().format('DD/MM/YYYY');
          element.paymentDate = element.paymentDate !== null ? moment.utc(element.paymentDate).local().format('DD/MM/YYYY hh:mm A') : "";
          element.deliveryTime = element.deliveryTime !== null ? moment.utc(element.deliveryTime).local().format('DD/MM/YYYY hh:mm A') : "";
        });

        this.items = results.clientOrders;
        this.originalItems = [...this.items];
        this.onHandleFilter(this.filterType);
      }, error: (error) => {
        this.showError(error);
      }
    });
  }

  onCancelOrder(event) {
    this.selectedItem = event;
    this.changeDetector.detectChanges();
    this.deleteModal.nativeElement.click();
  }

  onSetDriver(event) {
    this.selectedItem = event;
    this.changeDetector.detectChanges();
    this.setDriverModal.nativeElement.click();
  }

  onSetLocation(event) {
    this.selectedItem = event;
    this.changeDetector.detectChanges();
    this.setLocationModal.nativeElement.click();
  }

  onViewFeedback(event) {
    this.selectedItem = event;
    this.changeDetector.detectChanges();
    this.viewFeedbackModal.nativeElement.click();
  }

  onViewDetails(event) {
    this.storageService.secureStorage.setItem('order', event);
    this.onTogglePage('order-details/' + event.id)
  }

  onHandleFilter(event) {
    let items = [...this.originalItems];
    switch (event) {
      case 1:
        this.items = this.originalItems.filter(item =>  item.clientOrderStatus !== 1)
        break;
      case 2:
        items = items.filter(item => item.latitude === 0 && item.longitude === 0 && item.clientOrderStatus !== 1);
        this.items = [...items];
        break;
      case 3:
        items = items.filter(item => !item.driver && item.latitude !== 0 && item.longitude !== 0 && item.clientOrderStatus !== 1);
        this.items = [...items];
        break;
      case 4:
        items = items.filter(item => item.clientOrderStatus === 1);
        this.items = [...items];
        break;
      default:
        break;

    }
  }

  onClose() {
    this.viewGallery = false;
  }

  onViewGellary(item) {
    this._service.getClientOrderImagesById(item.id)
      .subscribe({
        next: (results) => {
          let images = [];
          results.clientOrderImages.forEach(element => {
            element.src = element.url;
            element.title = item.client;
            element.date = new Date(item.updated);
            images.push(element);
          });

          this.viewGallery = true;
          this.changeDetector.detectChanges();
          this.gallery.viewGallery(images);
        }, error: (error) => {
          this.showError(error);
        }
      });

  }


  onUnassignDriverForClientOrder(event) {
    Swal.fire({
      title: 'هل أنت متأكد؟',
      text: `هل أنت متأكد من ازالة السائق`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      cancelButtonText: 'الغاء',
      confirmButtonText: 'تأكيد'
    }).then((result) => {
      if (result.isConfirmed) {
        this.onSubmit(event);
      }
    })
  }

  onSubmit(event) {

    const deta = {
      "clientOrderId": event.id
    }
    this._service.unassignDriverForClientOrder(deta).subscribe(
      (id) => {
        this.showSuccess('تمت العملية بنجاح');
        this.get();
      },
      error => {
        this.showError(error);
      });


  }

  openMap(item) {
    const modalRef = this.ngModal.open(OrderManagementLocationComponent, {size: 'lg'});
    modalRef.componentInstance.markers = [{
      position: {
        lat: item.latitude,
        lng: item.longitude,
    },
    options: {
        icon: "assets/mosque.png",
        animation: google.maps.Animation.DROP,
    },
    }];
    modalRef.componentInstance.item = item
  }
}