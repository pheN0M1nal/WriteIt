using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext:IdentityDbContext<ApplicationUser>
{
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.writername)
                .HasMaxLength(20);
        }
}