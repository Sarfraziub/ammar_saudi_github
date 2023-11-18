import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-delete-modal',
    templateUrl: './delete-modal.component.html',
})
export class DeleteModalComponent {

    @Output() onConfirm = new EventEmitter<boolean>();
    private _selectedItem: any = null;
    @Input() get selectedItem() {
        return this._selectedItem;
    }

    set selectedItem(item: any) {
        this._selectedItem = item;
    }
 private _deleteTitle: string = 'هل أنت متأكد من الحذف';
    @Input() get deleteTitle() {
        return this._deleteTitle;
    }

    set deleteTitle(title: string) {
        this._deleteTitle = title;
    }
    onToggleConfirm() {
        this.onConfirm.emit(true);
    }
}
