using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperheroesApp.Models;

namespace SuperheroesApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Superhero> Superheroes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}