import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
    name: 'ListCheckboxPipe',
    pure: false
})
export class ListCheckboxPipe implements PipeTransform {

    transform(val: string): string {
        if (val == "true")
            return "checked";
        return "";
    }
}
