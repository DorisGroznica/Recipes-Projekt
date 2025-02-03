using Microsoft.EntityFrameworkCore;
using Recipes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Documents;
using Microsoft.AspNetCore.Authorization;


namespace Recipes.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Torte> Torte { get; set; }
        public DbSet<Kremasti> Kremastikolači { get; set; }
        public DbSet<Suhi> Suhikolači { get; set; }
        public DbSet<IdentityRole> IdentityUsers { get; set; }

     
    }


  
}
