export class Dashboard {
    MusteriSayisi: number;
    YeniCKlar: number;
    GenislemeHizi: number;
    BugunkuTahsilatlar: number;
    DunkuTahsilatlar: number;
    UretimdekiSiparisler: number;
    BekleyenSiparisler: number;
    ToplamKazanc: number;
    ToplamKazancGecenAy: number;
    ToplamKazancGecenHafta: number;
}

export class FlotChart {
    DataToplamSatislar: FlotChartItem;
    DataButce: FlotChartItem;
    Gelir: number;
    SonGunlerdeki: number;
    Talepler: string;
    ToplamSatislar: string;
    EnCok: string;
}

export class FlotChartItem {
    data: Array<any>;
    showLabels: boolean;
    label: string;
    labelPlacement: string;
    canvasRender: boolean;
    cColor: string;
    color: string;
}