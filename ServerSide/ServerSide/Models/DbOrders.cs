using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ServerSide.Models
{
    public class DbOrders
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }

        public DbUsers User { get; set; }
        public DbBooks Book { get; set; }
    }

    public class DbOrderConfiguration : IEntityTypeConfiguration<DbOrders>
    {
        public void Configure(EntityTypeBuilder<DbOrders> builder)
        {
            builder
                .ToTable("Orders");

            builder
                .HasKey(b => b.Id);

            builder
                .HasOne(o => o.User)
                .WithMany(b => b.Orders);

            builder
                .HasOne(o => o.Book)
                .WithMany(b => b.Orders);
        }
    }
}
