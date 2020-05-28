import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'DateTimeFormatPipe'
})
export class DateTimeFormatPipePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return null;
  }

}
