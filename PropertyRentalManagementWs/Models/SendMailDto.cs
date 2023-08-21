using System.ComponentModel.DataAnnotations;

namespace PropertyRentalManagementWs.Models
{
    public class SendMailDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]  
        public string Body { get; set; }    
    }
}
