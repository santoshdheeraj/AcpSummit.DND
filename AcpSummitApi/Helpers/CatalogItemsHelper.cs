using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcpSummitApi.Models;

namespace AcpSummitApi.Helpers
{
    public class CatalogItemsHelper
    {
        public CatalogItemsHelper()
        {

        }

        public string PrintItemsInTabularFormat(IEnumerator<CatalogItem> catalogItemEnumerator)
        {
            string rs = "";

            rs += $"{"Id".PadRight(40)} | {"Name".PadRight(15)} | {"Description".PadRight(50)} | {"Price"}" + "\n";
            rs += new string('-', rs.Length) + "\n";

            while (catalogItemEnumerator.MoveNext())
            {
                rs += catalogItemEnumerator.Current + "\n";
            }

            return rs;
        }
    }
}
