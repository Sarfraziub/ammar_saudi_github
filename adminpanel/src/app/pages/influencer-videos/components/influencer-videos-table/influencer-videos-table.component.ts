import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { Gallery } from 'app/shared/components';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { InfluencerVideosService } from 'app/shared/services';

@Component({
  selector: 'influencer-videos-table-cmp',
  templateUrl: './influencer-videos-table.html',
})
export class InfluencerVideosTableComponent extends BaseComponent implements OnInit {

  @ViewChild('viewModal') viewModal: any;
  @ViewChild('deleteModal') deleteModal: any;
  @ViewChild('gallery') gallery: Gallery;
  @ViewChild('videoModal') videoModal: any;


  selectedItem: any = null;
  items = [];
  constructor(private _service: InfluencerVideosService, private changeDetector: ChangeDetectorRef) {
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
          results.influencerVideos.forEach(element => {

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

  onDeleteRow() {
    this.deleteModal.nativeElement.click();
  }

  onEditRow(event) {
    this.viewModal.nativeElement.click();
  }

  onAddRow() {
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

  viewGallery: boolean;
  onViewGellary(item = null) {
    if (item) {
      this.selectedItem = item;
    }
    this.viewGallery = true;
    this.changeDetector.detectChanges();
    let items = item ? [item] : this.items.filter(f => f.contentType !== 2);
    if (item && this.selectedItem.contentType === 2) {
      this.videoModal.nativeElement.click();
    } else {
      this.gallery.viewGallery(items);
    }
  }

  onClose() {
    this.viewGallery = false;
  }
}