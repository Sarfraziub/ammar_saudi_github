import {
  Component,
  Input,
  Output,
  EventEmitter,
  ContentChildren,
  QueryList,
  TemplateRef,
  ContentChild,
  ViewChildren,
  OnInit,
  ViewChild
} from '@angular/core';
import { DataTableColumn } from './column.component';
import { DataTableRow } from './row.component';
import { DataTableParams } from './types';
import { RowCallback } from './types';
import { DataTableTranslations, defaultTranslations } from './types';
import { drag } from '../utils/drag';
import { BaseComponent } from 'app/shared/infrastructure/base.component';
import { DataTablePagination, DataTableResource } from '../angular-bootstrap-data-table.module';

@Component({
  selector: 'data-table',
  templateUrl: './table.html',
  styleUrls: ['./table.scss']
})
export class DataTable extends BaseComponent implements DataTableParams, OnInit {

  constructor() {
    super();
  }
  @ViewChild('paginationCmp') paginationCmp: DataTablePagination;
  @ContentChild('footerTemplate') footerTemplate;
  @ContentChild('captionTemplate') captionTemplate;
  private _items: any[] = [];
  private originalItems: any[] = [];
  @Input() get items() {
    return this._items;
  }

  set items(items: any[]) {
    this.selectedId = null;
    this.originalItems = [...items];
    if (!this.lazy) {
      this.setItemResource(items);
    }
    else {
      this._items = [...items];
      this.handleSelectedItems();
    }
    this._onReloadFinished();
  }

  @Input() itemCount: number;

  // UI components:

  @ContentChildren(DataTableColumn) columns: QueryList<DataTableColumn>;
  @ViewChildren(DataTableRow) rows: QueryList<DataTableRow>;
  @ContentChild('collapseTemplate') collapseTemplate: TemplateRef<any>;

  // One-time optional bindings with default values:

  @Input() headerTitle: string;
  @Input() header = false;
  @Input() pagination = true;
  @Input() indexColumn = false;
  @Input() indexColumnHeader = '';
  @Input() rowColors: RowCallback;
  @Input() rowTooltip: RowCallback;
  @Input() selectColumn = false;
  @Input() multiSelect = false;
  @Input() substituteRows = true;
  @Input() translations: DataTableTranslations = defaultTranslations;
  @Input() selectOnRowClick = false;
  @Input() autoReload = true;
  @Input() showReloading = false;
  @Input() className = '';
  @Input() lazy = false;
  @Input() exelExport = false;
  @Input() pdfExport = false;
  @Input() print = false;
  @Input() exportFileName = new Date().toDateString();
  @Input() collapse = false;
  @Input() filter = true;
  @Input() key: string = '';
  @Input() printTarget: string = '';
  @Input() tableDark: boolean = false;
  @Input() smallTable: boolean = true;
  @Input() activeTitle: string = 'Active';
  @Input() inActiveTitle: string = 'In-Active';


  @Input() allowDelete: boolean = true;
  @Input() allowEdit: boolean = true;
  @Input() allowAdd: boolean = true;
  @Input() allowRefresh: boolean = true;

  @Output() onRowSelect = new EventEmitter<any>();
  @Output() onDeleteRow = new EventEmitter<any>();
  @Output() onEditRow = new EventEmitter<any>();
  @Output() onAddRow = new EventEmitter<any>();
  @Output() onRefreshData = new EventEmitter<any>();


  // UI state without input:

  indexColumnVisible: boolean;
  selectColumnVisible: boolean;

  // UI state: visible ge/set for the outside with @Input for one-time initial values

  private _sortBy: string;
  private _sortAsc = true;

  private _offset = 0;
  private _limit = 10;

  @Input()
  get sortBy() {
    return this._sortBy;
  }

  set sortBy(value) {
    this._sortBy = value;
    this._triggerReload();
  }

  @Input()
  get sortAsc() {
    return this._sortAsc;
  }

  set sortAsc(value) {
    this._sortAsc = value;
    this._triggerReload();
  }

  @Input()
  get offset() {
    return this._offset;
  }

  set offset(value) {
    this._offset = value;
    this._triggerReload();
  }

  @Input()
  get limit() {
    return this._limit;
  }

  set limit(value) {
    this._limit = value;
    this._triggerReload();
  }

  // calculated property:

  @Input()
  get page() {
    return Math.floor(this.offset / this.limit) + 1;
  }

  set page(value) {
    this.offset = (value - 1) * this.limit;
  }

  get lastPage() {
    return Math.ceil(this.itemCount / this.limit);
  }

  // setting multiple observable properties simultaneously

  sort(sortBy: string, asc: boolean) {
    this.sortBy = sortBy;
    this.sortAsc = asc;
  }


  // init

  ngOnInit() {
    this._initDefaultValues();
    this._initDefaultClickEvents();
    this._updateDisplayParams();

    if (this.autoReload && this._scheduledReload == null && !this.lazy) {
      this.reloadItems();
    }
  }

  private _initDefaultValues() {
    this.indexColumnVisible = this.indexColumn;
    this.selectColumnVisible = this.selectColumn;
  }

  private _initDefaultClickEvents() {
    this.headerClick.subscribe(tableEvent =>
      this.sortColumn(tableEvent.column)
    );
    // if (this.selectOnRowClick) {
    //   this.rowClick.subscribe(
    //     tableEvent => (tableEvent.row.selected = !tableEvent.row.selected)
    //   );
    // }
  }

  // Reloading:

  _reloading = false;

  get reloading() {
    return this._reloading;
  }

  @Output() reload = new EventEmitter();

  @Output() lazyLoad = new EventEmitter();

  itemResource: any = null;
  setItemResource(items: any[]) {
    this.itemResource = new DataTableResource(items || []);
    if (this.paginationCmp) {
      this.paginationCmp.pageFirst();
    }
    else {
      this.itemResource.query({ offset: 0, limit: 10 }).then(items => {
        this._items = items
        this.handleSelectedItems();
      });
    }
  }

  reloadItems() {
    this._reloading = true;
    const params = this._getRemoteParameters();
    this.itemResource.query(params).then(items => {
      this._items = items;
      this.handleSelectedItems();
    });
  }

  lazyLoadItems(pageNumber) {
    this.lazyLoad.emit(pageNumber);
  }

  private _onReloadFinished() {
    this._updateDisplayParams();

    this._selectAllCheckbox = false;
    this._reloading = false;
  }

  _displayParams = <DataTableParams>{}; // params of the last finished reload

  get displayParams() {
    return this._displayParams;
  }

  _updateDisplayParams() {
    this._displayParams = {
      sortBy: this.sortBy,
      sortAsc: this.sortAsc,
      offset: this.offset,
      limit: this.limit
    };
  }

  _scheduledReload = null;

  // for avoiding cascading reloads if multiple params are set at once:
  _triggerReload() {
    if (this._scheduledReload) {
      clearTimeout(this._scheduledReload);
    }
    if (!this.lazy) {
      this._scheduledReload = setTimeout(() => {
        this.reloadItems();
      });
    }
  }

  // event handlers:

  @Output() rowClick = new EventEmitter();
  @Output() rowDoubleClick = new EventEmitter();
  @Output() headerClick = new EventEmitter();
  @Output() cellClick = new EventEmitter();

  @Output() OnMultiRowsSelected = new EventEmitter();

  public rowClicked(row: DataTableRow, event) {
    this.selectedRow = row;
    this.rowClick.emit({ row, event });

  }

  public rowDoubleClicked(row: DataTableRow, event) {
    this.rowDoubleClick.emit({ row, event });
  }

  public headerClicked(column: DataTableColumn, event: MouseEvent) {
    if (!this._resizeInProgress) {
      this.headerClick.emit({ column, event });
    } else {
      this._resizeInProgress = false; // this is because I can't prevent click from mousup of the drag end
    }
  }

  cellClicked(
    column: DataTableColumn,
    row: DataTableRow,
    event: MouseEvent
  ) {
    this.cellClick.emit({ row, column, event });
  }

  // functions:

  private _getRemoteParameters(): DataTableParams {
    let params = <DataTableParams>{};

    if (this.sortBy) {
      params.sortBy = this.sortBy;
      params.sortAsc = this.sortAsc;
    }
    if (this.pagination) {
      params.offset = this.offset;
      params.limit = this.limit;
    }
    return params;
  }

  private sortColumn(column: DataTableColumn) {
    if (column.sortable) {
      let ascending = this.sortBy === column.property ? !this.sortAsc : true;
      this.sort(column.property, ascending);
    }
  }

  get columnCount() {
    let count = 0;
    count += this.indexColumnVisible ? 1 : 0;
    count += this.selectColumnVisible ? 1 : 0;
    count += this.collapse ? 1 : 0;
    count += this.selectOnRowClick ? 1 : 0;
    this.columns.toArray().forEach(column => {
      count += column.visible ? 1 : 0;
    });
    return count;
  }

  public getRowColor(item: any, index: number, row: DataTableRow) {
    if (this.rowColors !== undefined) {
      return (<RowCallback>this.rowColors)(item, row, index);
    }
  }

  // selection:

  selectedRow: DataTableRow;
  selectedRows: DataTableRow[] = [];
  private _selectAllCheckbox = false;

  get selectAllCheckbox() {
    return this._selectAllCheckbox;
  }

  set selectAllCheckbox(value) {
    this._selectAllCheckbox = value;
    this._onSelectAllChanged(value);
  }

  private _onSelectAllChanged(value: boolean) {
    this.rows.toArray().forEach(row => {
      row.item.selected = value;
      this.onRowSelectChanged(row);
    });
  }

  onRowSelectChanged(row: any) {
    if (this.multiSelect) {
      let index = this.selectedRows.findIndex(f => f[this.key] === row.item[this.key]);
      if (row.item.selected && index < 0) {
        this.selectedRows.push(row.item);
      } else if (!row.item.selected && index >= 0) {
        this.selectedRows.splice(index, 1);
      }
      else if (index >= 0) {
        this.selectedRows[index] = row.item;
      }

      this.OnMultiRowsSelected.emit(this.selectedRows);
      if (this.originalItems.some(s => !s.selected)) {
        this._selectAllCheckbox = false;
      }
      else {
        this._selectAllCheckbox = true;
      }
    } else {
      if (row.item.selected) {
        this.selectedRow = row;
      } else if (this.selectedRow === row) {
        this.selectedRow = undefined;
      }
    }

    // unselect all other rows:
    if (row.item.selected && !this.multiSelect) {
      this.rows
        .toArray()
        .filter(row_ => row_.item.selected)
        .forEach(row_ => {
          if (row_ !== row) {
            // avoid endless loop
            row_.item.selected = false;
          }
        });
    }
  }

  handleSelectedItems() {
    if (!this.originalItems.some(f => !f.selected)) {
      this._selectAllCheckbox = true;
    }
    this.originalItems?.filter(f => f.selected)?.forEach(item => {
      this.onRowSelectChanged({ item: item });
    });
  }

  // filter:

  onFilter(event, column) {
    if (!this.isEmptyOrSpaces(event.target.value)) {
      if (typeof event.target.value == 'string') {
        event.target.value = event.target.value.toLowerCase()
      }
      this._items = [...this.originalItems.filter(f => f[column.property]?.toLowerCase().includes(event.target.value))];
    }
    else {
      if (!this.lazy) {
        this.reloadItems();
      }
      this._items = [...this.originalItems];
    }
  }

  // other:

  get substituteItems() {
    return Array.from({ length: this.displayParams.limit - this.items.length });
  }

  // column resizing:

  private _resizeInProgress = false;

  public resizeColumnStart(
    event: MouseEvent,
    column: DataTableColumn,
    columnElement: HTMLElement
  ) {
    this._resizeInProgress = true;

    drag(event, {
      move: (_moveEvent: MouseEvent, dx: number) => {
        if (this._isResizeInLimit(columnElement, dx)) {
          column.width = columnElement.offsetWidth + dx;
        }
      }
    });
  }

  onTogglePrint() {
    window.print();
  }

  selectedId: number = null;


  onToggleRow(item, index) {
    this.selectedId = index + 1 === this.selectedId ? null : index + 1;
    this.onRowSelect.emit(item);
  }

  onToggleAdd() {
    this.selectedId = null;
    this.onAddRow.emit();
  }

  onToggleEdit() {
    this.onEditRow.emit();
  }

  onToggleDelete() {
    this.onDeleteRow.emit();
  }

  onToggleRefresh() {
    this.selectedId = null;
    this.onRefreshData.emit(true);
  }


  resizeLimit = 30;

  private _isResizeInLimit(columnElement: HTMLElement, dx: number) {
    /* This is needed because CSS min-width didn't work on table-layout: fixed.
         Without the limits, resizing can make the next column disappear completely,
         and even increase the table width. The current implementation suffers from the fact,
         that offsetWidth sometimes contains out-of-date values. */
    if (
      (dx < 0 && columnElement.offsetWidth + dx <= this.resizeLimit) ||
      !columnElement.nextElementSibling || // resizing doesn't make sense for the last visible column
      (dx >= 0 &&
        (<HTMLElement>columnElement.nextElementSibling).offsetWidth + dx <=
        this.resizeLimit)
    ) {
      return false;
    }
    return true;
  }
}
