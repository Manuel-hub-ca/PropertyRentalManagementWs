using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PropertyRentalManagementWs.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int PotentialTenantId { get; set; }
        public int PropertyManagerId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }

        public virtual PotentialTenant PotentialTenant { get; set; }
        public virtual PropertyManager PropertyManager { get; set; }
    }
}
