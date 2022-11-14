using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MountHebronAppApi.Models;

namespace MountHebronAppApi.Context
{
    public class ApartmentContext: DbContext
    {
        public ApartmentContext(DbContextOptions<ApartmentContext> options): base(options)
        {

        }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<Member> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<JoinMember> RequestsJoin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
