import { InvoiceStatuses } from "app/billing/invoicestatuses/invoicestatuses";

export class Invoice {

	Status: InvoiceStatuses;


	GuestName: String;


	InvoiceNo: String;


	DateIn: Date;


	DateOut: Date;


	Id: number;


	CreatedDate: Date;


	DeletedDate: Date;


	InsertNew: Boolean;

}