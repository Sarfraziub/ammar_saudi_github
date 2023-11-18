import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'columnVisibilityPipe',
})

export class ColumnVisibilityPipe implements PipeTransform {

    constructor() {
    }
    transform(objects: any[]): any[] {
        if(objects) {
            return objects.filter(f => f.visible);
        }
    }

}