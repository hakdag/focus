using Focus.Common;
using Focus.Common.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Required = Focus.Common.Attributes.RequiredAttribute;

namespace NoRoom.Common
{
    [Module("billing", "Billing")]
    [Title("Invoice")]
    public class Invoice : BaseModel
    {
        public int GuestId { get; set; }

        [Display(Name = "Status")]
        [Required]
        public InvoiceStatuses Status { get; set; }

        [Display(Name = "Guest")]
        [ForeignKey("GuestId")]
        [Required]
        public virtual Guest Guest { get; set; }

        [Display(Name = "Invoice No")]
        [SearchProperty]
        [Required]
        public string InvoiceNo { get; set; }

        [Display(Name = "Date In")]
        [Required]
        public DateTime DateIn { get; set; }

        [Display(Name = "Date Out")]
        [Required]
        public DateTime DateOut { get; set; }
    }

    [Module("billing", "Billing")]
    [Title("Invoice Status")]
    public enum InvoiceStatuses
    {
        Open = 1,
        Closed,
        Cancel,
        Void
    }
}
