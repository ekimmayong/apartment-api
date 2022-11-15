using Microsoft.EntityFrameworkCore;
using MountHebronAppApi.Models;

namespace MountHebronAppApi.Context
{
    public class PropertyContext: DbContext
    {
        public PropertyContext(DbContextOptions<PropertyContext> options): base(options) { }

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
