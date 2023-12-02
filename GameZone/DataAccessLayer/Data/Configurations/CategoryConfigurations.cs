using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(C => C.Name).HasMaxLength(500);
            builder.HasData(new Category[]
            {
                new Category{ Id=1,Name="Sports"}, 
                new Category{ Id=2,Name="Acitons"}, 
                new Category{ Id=3,Name="Adventures"}, 
                new Category{ Id=4,Name="Sports"}, 
                new Category{ Id=5,Name="Fighting"}, 
                new Category{ Id=6,Name="Film"},
            });
        }
    }
}
