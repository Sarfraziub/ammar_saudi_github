import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'app-tree-view',
    templateUrl: './tree-view.component.html',
})
export class TreeViewComponent implements OnInit {
    nodes: any[] = [];
    searchControl: FormControl = new FormControl('');
    selectedItem: any;
    private _data: any[];
    private originalData: any[] = [];

    // Inputs 
    @Input('disableSearch') disableSearch: any[];
    @Input('isChild') isChild: boolean = false;
    @Input() get data() {
        return this._data;
    }
    set data(data: any[]) {
        this.originalData = [...data];
        this._data = [...data];
        this.initNodes();
    }

    // Outputs
    @Output() onSelect = new EventEmitter<any>();

    @Output() onOpen = new EventEmitter<any>();

    @Output() onClose = new EventEmitter<any>();

    constructor() { }

    ngOnInit() {
        this.initNodes();
    }

    initNodes(isClose: boolean = true) {
        const cloned = this.data.map(x => Object.assign([], x));
        this.nodes = this.isChild ? this.data : this.handleNodes(cloned, isClose);
    }

    open(node) {
        if (node?.children?.length) {
            node.isClose = false;
            node.isSelected = false;
            this.setSelectedItem();
            this.onOpen.emit(node);
        }
    }

    close(node) {
        if (node) {
            node.isClose = true;
            this.setSelectedItem();
            this.onClose.emit(node);
        }
    }

    select(node) {
        this.removeSelectionItem(this.nodes, node?.id);
        node.isSelected = !node.isSelected;
        this.selectedItem = { ...node };
        this.onSelect.emit(node);
    }

    onSearch() {
        if (!this.data && !this.data.length) {
            return;
        }
        const oldData = [...this._data];
        this._data = [];
        if (!this.searchControl.value?.trim()) {
            this._data = [...this.originalData];
            this.initNodes();
        }
        else {
            // hack
            let originalData = JSON.parse(JSON.stringify(this.originalData));
            originalData.forEach(value => {
                let label = value?.name || value?.label || value?.value;
                if (label && typeof label === 'string') {
                    if (label?.trim()?.toLowerCase().includes(this.searchControl.value?.trim()?.toLowerCase())) {
                        const parent = originalData.find(f => f?.id === value?.parentId);
                        const oldItem = oldData?.find(f => f?.id == value?.id);
                        if (value?.parentId && !this._data.find(f => f?.id === parent?.id)) {
                            let label = parent?.name || parent?.label || parent?.value;
                            parent.isClose = oldItem?.isClose;
                            parent.label = this.markSearchedValue(label);
                            this._data.push(parent);
                        }
                        if (!this._data.find(f => f?.id === value?.id)) {
                            value.label = this.markSearchedValue(label);
                            this._data.push(value);
                        }
                    }
                }
            });
            this.initNodes(false);
        }
        this.setSelectedItem();
    }

    private handleNodes(data, isClose: boolean = true) {
        let tree = [], lookup = {};

        data.forEach(element => {
            lookup[element?.id] = element;
            element.children = [];
            if (isClose) {
                element.isClose = isClose;
            }
        });

        data.forEach(element => {
            if (element?.parentId && lookup[element?.parentId]) {
                lookup[element?.parentId].children.push(element);
            } else {
                tree.push(element);
            }
        });
        this.setSelectedItem();
        return tree;
    }

    private markSearchedValue(value: string): string {
        if (value?.trim()?.toLowerCase().includes(this.searchControl.value?.trim()?.toLowerCase())) {
            const index = value.trim().toLowerCase().indexOf(this.searchControl.value?.trim()?.toLowerCase());
            return `${value.substring(0, index)}<mark>${value.substr(index, this.searchControl.value.length)}</mark>${value.substring(index + this.searchControl.value.length, value.length)}`
        }
        else {
            return value;
        }
    }

    private setSelectedItem(nodes: any[] = []) {
        if (!nodes?.length) {
            nodes = this.nodes;
        }
        nodes.forEach(item => {
            if (item?.id === this.selectedItem?.id) {
                item.isSelected = this.selectedItem?.isSelected;
            }
            if (item?.children?.length) {
                this.setSelectedItem(item?.children)
            }
        });
    }

    private removeSelectionItem(nodes: any[], ignoreId: number = null) {
        nodes.forEach(element => {
            if (element?.id !== ignoreId) {
                element.isSelected = false;
            }
            if (element.children?.length > 0) {
                this.removeSelectionItem(element.children);
            }
        });
    }
}
