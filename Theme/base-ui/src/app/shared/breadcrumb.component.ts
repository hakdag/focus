import { Component, ViewEncapsulation, Input } from '@angular/core';

@Component({
    selector: 'breadcrumb',
    templateUrl: './breadcrumb.template.html',
    encapsulation: ViewEncapsulation.None
})
export class BreadCrumb {
    @Input() public itemId: number;
    @Input() public lastText: string;
    
    private items: any[] = [];
    private tree = [{id: 1, text: '', class: 'fa fa-home', link: '/app/dashboard', parent: null, children: [
        {id: 2, text: 'Tanımlamalar', class: 'fa fa-table', parent: 1, children: [{
            id: 3, text: 'Araçlar', link: '/app/tanimlamalar/araclist', class: '', parent: 2, children: [{
                id: 4, text: '', class: '', parent: 3, children: []
            }]
        }, {
            id: 5, text: 'Adresler', link: '/app/tanimlamalar/adreslist', class: '', parent: 2, children: [{
                id: 6, text: '', class: '', parent: 5, children: []
            }]
        }, {
            id: 7, text: 'Depolar', link: '/app/tanimlamalar/depolist', class: '', parent: 2, children: [{
                id: 8, text: '', class: '', parent: 7, children: []
            }]
        }, {
            id: 9, text: 'Santraller', link: '/app/tanimlamalar/santrallist', class: '', parent: 2, children: [{
                id: 10, text: '', class: '', parent: 9, children: []
            }]
        }, {
            id: 11, text: 'Personeller', link: '/app/tanimlamalar/personellist', class: '', parent: 2, children: [{
                id: 12, text: '', class: '', parent: 11, children: []
            }]
        }, {
            id: 13, text: 'Güzergahlar', link: '/app/tanimlamalar/guzergahlist', class: '', parent: 2, children: [{
                id: 14, text: '', class: '', parent: 13, children: []
            }]
        }, {
            id: 15, text: 'Cari Kartlar', link: '/app/tanimlamalar/carikartlist', class: '', parent: 2, children: [{
                id: 16, text: '', class: '', parent: 15, children: []
            }]
        }, {
            id: 17, text: 'Mal ve Hizmetler', link: '/app/tanimlamalar/malhizmetlist', class: '', parent: 2, children: [{
                id: 18, text: '', class: '', parent: 17, children: []
            }]
        }]},
        {id: 19, text: 'Yönetim', class: 'fa fa-wrench', parent: 1, children: [
            {id: 20, text: 'Parametreler', link: '/app/yonetim/parametrelist', class: '', parent: 19, children: [{ id: 21, text: '', class: '', parent: 20, children: []}]},
            {id: 22, text: 'Kullanıcılar', link: '/app/yonetim/kullanicilist', class: '', parent: 19, children: [{ id: 23, text: '', class: '', parent: 22, children: []}]},
            {id: 24, text: 'Roller', link: '/app/yonetim/rollist', class: '', parent: 19, children: [{ id: 25, text: '', class: '', parent: 24, children: []}]},
            {id: 26, text: 'Aktivite Logları', link: '/app/yonetim/logs', class: '', parent: 19, children: [{ id: 27, text: '', class: '', parent: 26, children: []}]},
            {id: 28, text: 'Hata Logları', link: '/app/yonetim/errors', class: '', parent: 19, children: [{ id: 29, text: '', class: '', parent: 28, children: []}]}
        ]},
        {id: 30, text: 'Teklif', class: 'fa fa-thumbs-up', parent: 1, children: [
            {id: 31, text: 'Cari Kartlar', link: '/app/teklif/carikartlar', class: '', parent: 30, children: [{ id: 32, text: '', class: '', parent: 31, children: []}]},
            {id: 33, text: 'Sözleşmeler', link: '/app/teklif/sozlesmeler', class: '', parent: 30, children: [{ id: 34, text: '', class: '', parent: 33, children: []}]},
            {id: 35, text: 'Teklifler', link: '/app/teklif/teklifler', class: '', parent: 30, children: [{ id: 36, text: '', class: '', parent: 35, children: []}]}
        ]},
        {id: 37, text: 'Siparisler', class: 'fa fa-list', parent: 1, children: [
            {id: 38, text: 'Cari Kartlar', link: '/app/siparis/carikartlar', class: '', parent: 37, children: [{ id: 39, text: '', class: '', parent: 38, children: []}]},
            {id: 40, text: 'Siparişler', link: '/app/siparis/siparisler', class: '', parent: 37, children: [{ id: 41, text: '', class: '', parent: 40, children: []}]},
            {id: 42, text: 'Onay Bekleyenler', link: '/app/siparis/onaybekleyenler', class: '', parent: 37, children: [{ id: 43, text: '', class: '', parent: 42, children: []}]},
            {id: 44, text: 'Planlama', link: '/app/siparis/planlama', class: '', parent: 37, children: [{ id: 45, text: '', class: '', parent: 44, children: []}]}
        ]},
        {id: 46, text: 'Konkasör Tesisi', class: 'fa fa-bullhorn', parent: 1, children: [
            {id: 47, text: 'Araçlar', link: '/app/konkasor/konkasorler', class: '', parent: 46, children: [{ id: 48, text: '', class: '', parent: 47, children: []}]}
        ]},
        {id: 49, text: 'Üretim', class: 'fa fa-desktop', parent: 1, children: [
            {id: 50, text: 'Üretime Gönder', link: '/app/uretim/gonder', class: '', parent: 49, children: [{ id: 51, text: '', class: '', parent: 50, children: []}]},
            {id: 52, text: 'Devam Eden Üretimler', link: '/app/uretim/devameden', class: '', parent: 49, children: [{ id: 53, text: '', class: '', parent: 52, children: []}]},
        ]},
        {id: 54, text: 'Müşteri Takip', class: 'fa fa-users', parent: 1, children: [
            {id: 55, text: 'Cari Kartlar', link: '/app/musteri/carikartlar', class: '', parent: 54, children: [{ id: 56, text: '', class: '', parent: 55, children: []}]}
        ]},
        {id: 57, text: 'Tahsilat', class: 'fa fa-turkish-lira', parent: 1, children: [
            {id: 58, text: 'Teslim Edilmiş Siparişler', link: '/app/tahsilat/teslimedilmissiparisler', class: '', parent: 57, children: [{ id: 59, text: '', class: '', parent: 58, children: []}]},
            {id: 60, text: 'Çek ve Senet', link: '/app/tahsilat/cekvesenetler', class: '', parent: 57, children: [{ id: 61, text: '', class: '', parent: 60, children: []}]}
        ]},
        {id: 61, text: 'Personel', class: 'fa fa-user', parent: 1, children: [
            {id: 62, text: 'Personeller', link: '/app/personel/personeller', class: '', parent: 61, children: [{ id: 63, text: '', class: '', parent: 62, children: []}]},
            {id: 64, text: 'Avanslar', link: '/app/personel/avanslar', class: '', parent: 61, children: [{ id: 65, text: '', class: '', parent: 64, children: []}]},
            {id: 66, text: 'Günlük İşe Devam', link: '/app/personel/gunlukisedevamlist', class: '', parent: 61, children: [{ id: 67, text: '', class: '', parent: 66, children: []}]},
            {id: 68, text: 'Saatlik İzinler', link: '/app/personel/saatlikizinlist', class: '', parent: 61, children: [{ id: 69, text: '', class: '', parent: 68, children: []}]},
            {id: 70, text: 'Yıllık İzinler', link: '/app/personel/yillikizinlist', class: '', parent: 61, children: [{ id: 71, text: '', class: '', parent: 70, children: []}]}
        ]},
        {id: 72, text: 'Araç Takip', class: 'fa fa-car', parent: 1, children: [
            {id: 73, text: 'Araçlar', link: '/app/aractakip/araclar', class: '', parent: 72, children: [{ id: 74, text: '', class: '', parent: 73, children: []}]},
            {id: 75, text: 'Mikser Giriş', link: '/app/aractakip/miksergiris', class: '', parent: 72, children: [{ id: 76, text: '', class: '', parent: 75, children: []}]},
            {id: 77, text: 'Mikser Çıkış', link: '/app/aractakip/miksercikis', class: '', parent: 72, children: [{ id: 78, text: '', class: '', parent: 77, children: []}]}
        ]},
        {id: 79, text: 'Stok', class: 'fa fa-list-alt', parent: 1, children: [
            {id: 80, text: 'Depolar', link: '/app/stok/depolar', class: '', parent: 79, children: [{ id: 81, text: '', class: '', parent: 80, children: []}]},
            {id: 82, text: 'Yakıt Alımı', link: '/app/stok/yakitalimi', class: '', parent: 79, children: [{ id: 83, text: '', class: '', parent: 83, children: []}]},
            {id: 84, text: 'Mutfak Masrafları', link: '/app/stok/mutfakmasrafilist', class: '', parent: 79, children: [{ id: 85, text: '', class: '', parent: 84, children: []}]}
        ]},
    ]}];

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
