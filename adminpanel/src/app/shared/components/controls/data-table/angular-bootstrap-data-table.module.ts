import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { DataTable } from './components/table.component';
import { DataTableColumn } from './components/column.component';
import { DataTableRow } from './components/row.component';
import { DataTablePagination } from './components/pagination.component';
import { DataTableHeader } from './components/header.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { PixelConverter } from './utils/px';
import { Hide } from './utils/hide';
import { MinPipe } from './utils/min';
import { NumberToArray } from './utils/loop-number.pipe';

export * from './components/types';
export * from './tools/data-table-resource';

export {
  DataTable,
  DataTableColumn,
  DataTableRow,
  DataTablePagination,
  DataTableHeader
};
export const DATA_TABLE_DIRECTIVES = [DataTable, DataTableColumn];

@NgModule({
  imports: [CommonModule, FormsModule, NgbModule],
  declarations: [
    DataTable,
    DataTableColumn,
    DataTableRow,
    DataTablePagination,
    DataTableHeader,
    PixelConverter,
    Hide,
    MinPipe,
    NumberToArray,

  ],
  exports: [DataTable, DataTableColumn]
})
export class DataTableModule { }
