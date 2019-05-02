using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace OrderManagment.API.Mappings
{
    public class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public CategoryMapping()
        {
            this.HasKey(a => a.id);

            this.Property(a => a.name).HasMaxLength(200).HasColumnType("varchar").IsOptional();
            this.Property(a => a.UpdatedDate).HasColumnType("datetime2").IsOptional();
            this.Property(a => a.CreateDate).HasColumnType("datetime2").IsOptional();

            
        }
    }
}