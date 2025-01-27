using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SciqusTraining.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "ec5f2224-4c4d-49dd-91cd-c94212edab1e";
            var writerRoleId = "338687b7-5bf7-4584-86ef-3e2f36b9ccf9";

            var roles = new List<IdentityRole> 
            {
                 new IdentityRole
                 {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER"
                 },
                  new IdentityRole
                  {
                      Id = writerRoleId,
                      ConcurrencyStamp = writerRoleId,
                      Name = "Writer",
                      NormalizedName = "WRITER"
                  }
            };

            // Correctly seed roles into the IdentityRole entity
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
    
}
