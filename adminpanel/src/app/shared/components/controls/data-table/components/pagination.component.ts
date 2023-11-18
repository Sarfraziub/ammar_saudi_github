import { Component, Inject, forwardRef, ViewEncapsulation } from '@angular/core';
import { DataTable } from './table.component';

@Component({
  selector: 'data-table-pagination',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./pagination.scss'],
  templateUrl: './pagination.html'
})
export class DataTablePagination {
  constructor(
    @Inject(forwardRef(() => DataTable)) public dataTable: DataTable
  ) { }

  pageBack() {
    this.dataTable.offset -= Math.min(
      this.dataTable.limit,
      this.dataTable.offset
    );
    this.checkLazyLoad();
  }

  pageForward() {
    this.dataTable.offset += this.dataTable.limit;
    this.checkLazyLoad();
  }

  pageFirst() {
    this.dataTable.offset = 0;
    this.checkLazyLoad();
  }

  private checkLazyLoad() {
    if (this.dataTable.lazy) {
      this.dataTable.lazyLoadItems(this.page);
    }
  }

  pageLast() {
    this.dataTable.offset = (this.maxPage - 1) * this.dataTable.limit;
    this.checkLazyLoad();
  }

  get maxPage() {
    return Math.ceil(this.dataTable.itemCount / this.dataTable.limit);
  }

  get limit() {
    return this.dataTable.limit;
  }

  set limit(value) {
    this.dataTable.limit = Number(<any>value); // TODO better way to handle that value of number <input> is string?
  }

  get page() {
    return this.dataTable.page;
  }

  set page(value) {
    this.dataTable.page = Number(<any>value);
    this.checkLazyLoad();
  }
}
