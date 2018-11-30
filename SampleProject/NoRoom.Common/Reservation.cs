using System;
using System.Collections.Generic;
using Focus.Common;
using Focus.Common.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Required = Focus.Common.Attributes.RequiredAttribute;

namespace NoRoom.Common
{
    [Module("booking", "Reserve/Book")]
    [Title("Reservation")]
    public class Reservation : BaseModel
    {
        public int GuestId { get; set; }

        [Display(Name = "Guest")]
        [SearchProperty]
        [ForeignKey("GuestId")]
        [Required]
        public virtual Guest Guest { get; set; }

        [Display(Name = "KDV")]
        [DefaultValue(18)]
        [Required]
        public int NumberOfGuest { get; set; }

        [Display(Name = "Telephone")]
        [Required]
        public string Telephone { get; set; }

        [Display(Name = "Reservation Made By")]
        public string ReservationMadeBy { get; set; }

        [Display(Name = "Arrival Date")]
        [Required]
        public DateTime ArrivalDate { get; set; }

        [Display(Name = "Departure Date")]
        [Required]
        public DateTime DepartureDate { get; set; }

        [Display(Name = "Guests")]
        public virtual ICollection<Guest> Guests { get; set; }
    }
}
