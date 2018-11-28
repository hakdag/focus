using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Focus.Common;
using Focus.Common.Attributes;
using Required = Focus.Common.Attributes.RequiredAttribute;

namespace NoRoom.Common
{
    [Module("booking", "Reserve/Book")]
    [Title("Check In")]
    public class CheckIn : BaseModel
    {
        [Display(Name = "Guest Name")]
        [SearchProperty]
        [Required]
        public string GuestName { get; set; }

        [Display(Name = "Check In Number")]
        [Required]
        public int CheckInNumber { get; set; }

        [Display(Name = "Invoice No")]
        [Required]
        public string InvoiceNo { get; set; }

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
