import { Component, EventEmitter, HostListener, Output, QueryList, ViewChildren, ViewEncapsulation } from '@angular/core';
@Component({
    selector: 'gallery',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./gallery.component.scss'],
    templateUrl: './gallery.html',
})
export class Gallery {

    @Output() onClose = new EventEmitter<boolean>();

    images: any[] = [];

    @ViewChildren('galleryImages') things: QueryList<any>;

    ngAfterViewInit() {
        this.things.changes.subscribe(t => {
            this.ngForRendered();
        })
    }

    private ngForRendered() {
        
        var $initScope: any = $('#js-lightgallery');
        const self = this;
        if ($initScope.length) {
            $initScope.justifiedGallery(
                {
                    border: -1,
                    rowHeight: 150,
                    margins: 8,
                    waitThumbnailsLoad: true,
                    randomize: false,
                }).on('jg.complete', function () {
                    $initScope.lightGallery(
                        {
                            thumbnail: true,
                            animateThumb: true,
                            showThumbByDefault: true,
                        });
                    $("#js-lightgallery a:first-child > img").trigger("click");
                });
        };
        $initScope.on('onAfterOpen.lg', function (event) {
            $('body').addClass("overflow-hidden");
        });
        $initScope.on('onCloseAfter.lg', function (event) {
            self.onClose.emit(true);
            $('body').removeClass("overflow-hidden");
        });
        
    }


    viewGallery(images) {
        this.images = images;
    }

    onToggleClose() {
        this.onClose.emit(true);
    }
}


