using Focus.Common;
using Focus.Common.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Required = Focus.Common.Attributes.RequiredAttribute;

namespace NoRoom.Common
{
    [Module("booking", "Reserve/Book")]
    [Title("Check In")]
    public class CheckIn : BaseModel
    {
        public int GuestId { get; set; }
        public int InvoiceId { get; set; }

        [Display(Name = "Guest")]
        [SearchProperty]
        [ForeignKey("GuestId")]
        [Required]
        public virtual Guest Guest { get; set; }

        [Display(Name = "Check In Number")]
        [Required]
        public int CheckInNumber { get; set; }

        [Display(Name = "Invoice")]
        [ForeignKey("InvoiceId")]
        [Required]
        public virtual Invoice Invoice { get; set; }

        [Display(Name = "Arrival Date")]
        [Required]
        public DateTime ArrivalDate { get; set; }

        [Display(Name = "Departure Date")]
        [Required]
        public DateTime DepartureDate { get; set; }

        [Display(Name = "Adults")]
        [Required]
        public int Adults { get; set; }

        [Display(Name = "Infant")]
        public int? Infant { get; set; }

        [Display(Name = "Child")]
        public int? Child { get; set; }

        [Display(Name = "Baby")]
        public int? Baby { get; set; }

        [Display(Name = "Room Number")]
        [Required]
        public int RoomNumber { get; set; }
    }
}
