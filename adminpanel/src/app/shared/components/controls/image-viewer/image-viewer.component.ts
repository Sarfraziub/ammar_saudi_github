import { Component, EventEmitter, Input, Output, ViewChild, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'image-viewer',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./image-viewer.component.scss'],
    templateUrl: './image-viewer.component.html',
})
export class ImageViewer {
    @Output() onRemoveImage = new EventEmitter<any>();

    @ViewChild('uploadFile') uploadFile: any;
    @ViewChild('multiUpload') multiUpload: any;

    private _attachementTitle: string = 'Attachement';
    attachments: any[] = [];

    @Input() multiple = true;
    @Input() get attachementTitle() {
        return this._attachementTitle;
    }
    set attachementTitle(attachementTitle: string) {
        this._attachementTitle = attachementTitle;
    }

    private _classes: any[] = [];
    @Input() get classes() {
        return this._classes;
    }
    set classes(classes: any[]) {
        this._classes = classes;
    }

    onToggleUpload() {
        this.uploadFile.nativeElement.click();
    }

    onToggleUploadMultiple() {
        this.multiUpload.nativeElement.click();
    }

    onSelect(event) {
        if (event.target.files && event.target.files[0]) {
            var filesAmount = event.target.files.length;

            for (let i = 0; i < filesAmount; i++) {
                if (!this.attachments.find(f => f.src?.name === event.target.files[i].name)) {
                    const reader = new FileReader();
                    reader.onload = (e: any) => {
                        this.attachments.push({ file: e.target.result, src: event.target.files[i] });
                    };
                    reader.readAsDataURL(event.target.files[i]);
                }
            }
        }
    }

    onRemove(item) {
        this.onRemoveImage.emit(item);
    }

    removeItem(item) {
        this.attachments = this.attachments.filter(f => f !== item);
    }
}