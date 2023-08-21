using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PropertyRentalManagementWs.Models
{
    public partial class Apartment
    {
        public Apartment()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public int BuildingId { get; set; }
        public int NumberOfRooms { get; set; }
        public decimal RentPrice { get; set; }
        public string Status { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }

        public virtual Building Building { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
