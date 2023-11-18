import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { Gallery } from 'app/shared/components';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { HomePageIconsService, SliderItemService } from 'app/shared/services';

@Component({
  selector: 'home-page-icon-management-table-cmp',
  templateUrl: './home-page-icon-management-table.html',
})
export class HomePageIconManagementTableComponent extends BaseComponent implements OnInit {

  @ViewChild('viewModal') viewModal: any;
  @ViewChild('deleteModal') deleteModal: any;
  @ViewChild('gallery') gallery: Gallery;

  selectedItem: any = null;
  items = [];
  constructor(private _service: HomePageIconsService, private changeDetector: ChangeDetectorRef) {
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
          let items = [];
          results.homePageIcons.forEach(element => {

            element.src = element.imageUrl;
            element.title = element.title;
            element.date = new Date(element.created);
            items.push(element);
          });

          this.items = [...items];
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
    this.viewModal.nativeElement.click();
  }

  viewGallery: boolean;
  onViewGellary(item = null) {
    this.viewGallery = true;
    this.changeDetector.detectChanges();
    const items = item ? [item] : this.items
    this.gallery.viewGallery(items);
  }

  onClose() {
    this.viewGallery = false;
  }
}