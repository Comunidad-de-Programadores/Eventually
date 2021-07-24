using Eventually.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eventually
{
    public class ApplicationDbContext :
        IdentityDbContext<User>
    {
        public ApplicationDbContext
            (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserArea>().HasKey(userArea =>
            new { userArea.UserId, userArea.AreaId });

            builder.Entity<Area>().HasData(
               new Area
               {
                   Id = 1,
                   Name = "Programacion"
               },
               new Area
               {
                   Id = 2,
                   Name = "Dibujo Digital"
               },
               new Area
               {
                   Id = 3,
                   Name = "Dibujo Tradicional"
               },
               new Area
               {
                   Id = 4,
                   Name = "Animacion 3D"
               },
               new Area
               {
                   Id = 5,
                   Name = "Modelado 3D"
               });
        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<UserArea> UserAreas { get; set; }
    }
}
