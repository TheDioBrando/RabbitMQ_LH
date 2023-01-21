using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ServerSide.Models
{
    public class DbLibraries
    {
        public Guid Id { get; set; }
        public string Address { get; set; }

        public ICollection<DbBooks> Books { get; set; }

        public DbLibraries()
        {
            Books = new HashSet<DbBooks>();
        }
    }

    public class DbLibraryConfiguration : IEntityTypeConfiguration<DbLibraries>
    {
        public void Configure(EntityTypeBuilder<DbLibraries> builder)
        {
            builder
                .ToTable("Libraries");

            builder
                .HasKey(b => b.Id);

            builder
                .HasMany(o => o.Books)
                .WithOne(b => b.Library);
        }
    }
}
