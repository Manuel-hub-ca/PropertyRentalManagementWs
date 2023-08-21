using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PropertyRentalManagementWs.Models
{
    public partial class Building
    {
        public Building()
        {
            Apartment = new HashSet<Apartment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int PropertyManagerId { get; set; }

        public virtual PropertyManager PropertyManager { get; set; }
        public virtual ICollection<Apartment> Apartment { get; set; }
    }
}
