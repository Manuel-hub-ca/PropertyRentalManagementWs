using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PropertyRentalManagementWs.Models
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public int PotentialTenantId { get; set; }
        public int PropertyManagerId { get; set; }
        public int ApartmentId { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual PotentialTenant PotentialTenant { get; set; }
        public virtual PropertyManager PropertyManager { get; set; }
    }
}
