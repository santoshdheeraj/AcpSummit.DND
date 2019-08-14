using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcpSummitApi.Models
{
    public class CatalogItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Id.PadRight(40)} | {Name.PadRight(15)} | {Description.PadRight(50)} | {Price}";
        }

        public CatalogItem() { }
    }
}
