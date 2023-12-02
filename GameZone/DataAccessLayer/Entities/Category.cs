using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Category:BaseEntity
    {
        ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
