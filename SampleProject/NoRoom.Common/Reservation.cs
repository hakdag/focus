using System;
using Focus.Common;
using Focus.Common.Attributes;
using System.ComponentModel.DataAnnotations;
using Required = Focus.Common.Attributes.RequiredAttribute;

namespace NoRoom.Common
{
    [Module("booking", "Reserve/Book")]
    [Title("Reservation")]
    public class Reservation : BaseModel
    {
        [Display(Name = "Guest Name")]
        [SearchProperty]
        [Required]
        public string GuestName { get; set; }

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
    }
}
