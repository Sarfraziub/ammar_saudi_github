import { Component, Input, ViewChild, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'upload-file',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./upload-file.component.scss'],
    templateUrl: './upload-file.component.html',
})
export class UploadFile {

    @ViewChild('uploadFile') uploadFile: any;
    private _attachementTitle: string = 'Attachement';

    @Input() multiple = false;
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

    onToggleUpload(){
        this.uploadFile.nativeElement.click();
    }

    attachments = [];

    onSelect(event) {
        if (event.target.files && event.target.files[0]) {
            var filesAmount = event.target.files.length;

            for (let i = 0; i < filesAmount; i++) {
                if (this.attachments.find(f => f.src.name === event.target.files[i].name)) {
                } else {
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
        this.attachments = this.attachments.filter(f => f !== item);
    }
}