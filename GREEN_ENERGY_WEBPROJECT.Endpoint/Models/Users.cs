using Microsoft.AspNetCore.Identity;

namespace GREEN_ENERGY_WEBPROJECT.Endpoint.Models
{
    public class Users : IdentityUser
    {
        public bool Factory { get; set; }
        public string FullName { get; set; } 
    }
}
