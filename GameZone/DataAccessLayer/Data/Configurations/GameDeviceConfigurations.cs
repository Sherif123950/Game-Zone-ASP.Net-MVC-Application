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
    internal class GameDeviceConfigurations : IEntityTypeConfiguration<GameDevice>
    {
        public void Configure(EntityTypeBuilder<GameDevice> builder)
        {
            builder.HasKey(GD => new { GD.DeviceId,GD.GameId});
        }
    }
}
