using GREEN_ENERGY_WEBPROJECT.Endpoint.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GREEN_ENERGY_WEBPROJECT.Endpoint.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

       
    }
}

