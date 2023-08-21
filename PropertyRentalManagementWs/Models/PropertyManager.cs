using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PropertyRentalManagementWs.Models
{
    public partial class PropertyManager
    {
        public PropertyManager()
        {
            Appointment = new HashSet<Appointment>();
            Building = new HashSet<Building>();
            Message = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual ICollection<Building> Building { get; set; }
        public virtual ICollection<Message> Message { get; set; }
    }
}
