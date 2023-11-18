import { Pipe, PipeTransform } from '@angular/core';
import { layoutPaths } from 'app/theme.constants';

@Pipe({ name: 'baProfilePicture' })

export class ProfilePicturePipe implements PipeTransform {

  transform(input: string, ext = 'png'): string {
    return layoutPaths.images.root + input + '.' + ext;
  }
}