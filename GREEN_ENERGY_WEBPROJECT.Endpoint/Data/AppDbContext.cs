using GREEN_ENERGY_WEBPROJECT.Endpoint.Models;
using GREEN_ENERGY_WEBPROJECT.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GREEN_ENERGY_WEBPROJECT.Endpoint.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<HomeContent> HomeContents { get; set; }

        public DbSet<FirstDiagramContent> FirstDiagramContents { get; set; }
        public DbSet<SecondDiagramContent> SecondDiagramContents { get; set; }
        public DbSet<ThirdDiagramContent> ThirdDiagramContents { get; set; }
        public DbSet<FourthDiagramContent> FourthDiagramContents { get; set; }
        public DbSet<FifthDiagramContent> FifthDiagramContents { get; set; }
        public DbSet<SixthDiagramContent> SixthDiagramContents { get; set; }




    }
}

