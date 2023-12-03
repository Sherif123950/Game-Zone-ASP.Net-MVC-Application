using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Game:BaseEntity
    {
        public string Descripiton { get; set; } = string.Empty;
        public string CoverName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public ICollection<GameDevice> Devices { get; set; }=new List<GameDevice>();
    }
}
