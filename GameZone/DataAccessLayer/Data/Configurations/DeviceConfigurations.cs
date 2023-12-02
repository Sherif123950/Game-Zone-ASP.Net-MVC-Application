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
    public class DeviceConfigurations : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.Property(D => D.Name).HasMaxLength(500);
            builder.Property(D => D.Icon).HasMaxLength(500);
            builder.HasData(new Device[]
            {
                new Device{ Id=1,Name="PlayStation",Icon="bi bi-playstation"},
                new Device{ Id=2,Name="Xbox",Icon="bi bi-xboc"},
                new Device{ Id=3,Name="Nintendo Switch",Icon="bi bi-nintendo-switch"},
                new Device{ Id=4,Name="PC",Icon="bi bi-pc-display"},
            });
        }
    }
}
