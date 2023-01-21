using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServerSide.Models
{
    public class DbBooks
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid LibraryId { get; set; }

        public DbLibraries Library { get; set; }
        public ICollection<DbOrders> Orders { get; set; }

        public DbBooks()
        {
                Orders = new HashSet<DbOrders>();
        }
    }

    public class DbBookConfiguration : IEntityTypeConfiguration<DbBooks>
    {
        public void Configure(EntityTypeBuilder<DbBooks> builder)
        {
            builder
                .ToTable("Books");

            builder
                .HasKey(b => b.Id);

            builder
                .HasMany(o => o.Orders)
                .WithOne(b => b.Book);

            builder
                .HasOne(l => l.Library)
                .WithMany(b => b.Books);
        }
    }
}
