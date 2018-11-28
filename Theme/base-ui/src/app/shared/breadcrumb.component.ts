import { Component, ViewEncapsulation, Input } from '@angular/core';

import { NavigationTree } from './navigation-tree';

@Component({
    selector: 'breadcrumb',
    templateUrl: './breadcrumb.template.html',
    encapsulation: ViewEncapsulation.None
})
export class BreadCrumb {
    @Input() public itemId: number;
    @Input() public lastText: string;
    
    private items: any[] = [];
    private tree = NavigationTree.Tree;

    constructor() {}

    ngOnInit(): void {
        let item = this.findItem(this.itemId);
        if (this.lastText)
            item.text = this.lastText;
        this.buildBreadCrumb(item);
    }

    findItem(itemId: number): any {
        let root = this.tree[0];
        return this.traverseTree(root, itemId);
    }

    traverseTree(item: any, itemId: number): any {
        if (item.id == itemId)
            return item;

        for(let i = 0; i < item.children.length; i++) {
            item.children[i].parent = item;

            let child = this.traverseTree(item.children[i], itemId);
            if (child != null)
                return child;
        }

        return null;
    }

    buildBreadCrumb(item: any): void {
        let stack: any[] = [];
        this.buildStack(stack, item);
        for(let i = stack.length - 1; i >= 0; i--)
            this.items.push(stack[i]);
    }

    buildStack(stack: any[], item: any): void {
        if (item == null)
            return;
        stack.push(item);
        this.buildStack(stack, item.parent);
    }
}
