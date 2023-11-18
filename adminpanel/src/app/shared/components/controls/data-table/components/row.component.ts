import {
  Component,
  Input,
  Inject,
  forwardRef,
  Output,
  EventEmitter,
  OnDestroy
} from '@angular/core';
import { RowStatus } from 'app/shared/enum';
import { DataTable } from './table.component';

@Component({
  selector: '[dataTableRow]',
  templateUrl: './row.html',
  styleUrls: ['./row.scss']
})
export class DataTableRow implements OnDestroy {
  @Input() item: any;
  @Input() index: number;

  rowStatus: typeof RowStatus = RowStatus;

  expanded: boolean;

  // row selection:

  //private _selected: boolean;

  @Output() selectedChange = new EventEmitter();

  // get selected() {
  //   return this._selected;
  // }

  // set selected(selected) {
  //   this._selected = selected;
  //   this.selectedChange.emit(selected);
  // }

  onMultiRowSelected(event){
    this.selectedChange.emit(event);
  }

  // other:

  get displayIndex() {
    if (this.dataTable.pagination) {
      return this.dataTable.displayParams.offset + this.index + 1;
    } else {
      return this.index + 1;
    }
  }

  getTooltip() {
    if (this.dataTable.rowTooltip) {
      return this.dataTable.rowTooltip(this.item, this, this.index);
    }
    return '';
  }

  getSortClass(prop :string , index :number) :string{
    if(this.dataTable.sortBy === prop && index % 2 === 0){
      return 'even-sorting';
    }
    else if(this.dataTable.sortBy === prop && index % 2 !== 0){
      return 'odd-sorting';
    }
    return '';
  }

  constructor(
    @Inject(forwardRef(() => DataTable)) public dataTable: DataTable
  ) {
  }

  ngOnDestroy() {
    //this.selected = false;
  }

  public _this = this; // FIXME is there no template keyword for this in angular 2?
}
