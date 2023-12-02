using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Device:BaseEntity
    {
        public string Icon { get; set; } = string.Empty;
        public ICollection<GameDevice> Games { get; set; } = new List<GameDevice>();
    }
}
