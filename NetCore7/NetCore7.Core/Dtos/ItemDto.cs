using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class ItemExtendedDto: ItemDto
    {
        public string Description { get; set; }

    }
}
