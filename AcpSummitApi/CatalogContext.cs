using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcpSummitApi.Models;

namespace AcpSummitApi
{
    public class CatalogContext
    {
        public CatalogContext()
        {

        }

        public List<CatalogItem> CatalogItems { get; set; }
    }
}
