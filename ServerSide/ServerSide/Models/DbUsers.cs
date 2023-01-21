using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ServerSide.Models
{
    public class DbUsers
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<DbOrders> Orders { get; set; }

        public DbUsers()
        {
                Orders = new HashSet<DbOrders>();
        }
    }

    public class DbUesrConfiguration : IEntityTypeConfiguration<DbUsers>
    {
        public void Configure(EntityTypeBuilder<DbUsers> builder)
        {
            builder
                .ToTable("Users");

            builder
                .HasKey(b => b.Id);

            builder
                .HasMany(o => o.Orders)
                .WithOne(b => b.User);
        }
    }
}
