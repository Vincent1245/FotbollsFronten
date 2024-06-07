using FotbollsFronten.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FotbollsFronten.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Models.Blog> Blog { get; set; }
        public DbSet<Models.Comment> Comment  { get; set; }

        public DbSet<Models.User> User { get; set; }
        public DbSet<Models.Message> Messages { get; set; }

        //public DbSet<Models.Category> Categories { get; set; }

        public DbSet<Models.Report> Reports { get; set; }
    }

}
