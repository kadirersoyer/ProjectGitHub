using OrderManagment.Entities.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OrderManagment.API.Mappings
{
    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public ProductMapping()
        {
            this.HasKey(a => a.id);

            this.Property(a => a.name).HasMaxLength(200).IsOptional();
            this.Property(a => a.UpdatedDate).HasColumnType("datetime2").IsOptional();
            this.Property(a => a.CreateDate).HasColumnType("datetime2").IsOptional();
            this.Property(a => a.Price).HasPrecision(12, 6).IsOptional();

            this.HasRequired(a => a.Category).WithMany(a => a.Products).HasForeignKey(a => a.CategoryID);
        }
    }
}