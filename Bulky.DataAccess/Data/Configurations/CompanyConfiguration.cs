using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BulkyBook.DataAccess.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasData(
                  new Company
                  {
                      Id = 1,
                      Name = "Tech Solution",
                      StreetAddress = "123 Tech St",
                      City = "Tech City",
                      State = "IL",
                      PostalCode = "12121",
                      PhoneNumber = "01247859748"
                  },
                  new Company
                  {
                      Id = 2,
                      Name = "Vivid Books",
                      StreetAddress = "999 vid St",
                      City = "Vid City",
                      State = "IL",
                      PostalCode = "66666",
                      PhoneNumber = "10215787652"
                  },
                   new Company
                   {
                       Id = 3,
                       Name = "Readers Club",
                       StreetAddress = "999 Main St",
                       City = "Lala Land",
                       State = "NY",
                       PostalCode = "99999",
                       PhoneNumber = "01148893754"
                   }
            );
        }
    }
}
