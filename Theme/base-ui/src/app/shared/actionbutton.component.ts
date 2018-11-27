import { Component, ViewEncapsulation, Input } from '@angular/core';

@Component({
    selector: 'action-button',
    templateUrl: './actionbutton.template.html',
    encapsulation: ViewEncapsulation.None
})
export class ActionButton {
    @Input() public type: string;
    @Input() public class: string;
    @Input() public text: string;
    @Input() public icon: string;
    
    constructor() {}

    ngOnInit(): void {}

}