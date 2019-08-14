using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcpSummitApi.Helpers;
using AcpSummitApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcpSummitApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : Controller
    {
        public CatalogContextSeed _contextSeed = new CatalogContextSeed();
        public CatalogContext _catalogContext;

        public CatalogController()
        {
            _catalogContext = new CatalogContext();
            _catalogContext.CatalogItems = new List<CatalogItem>();
            if (!_catalogContext.CatalogItems.Any())
            {
                _contextSeed.SeedCatalogItems(_catalogContext, "csv");
            }
        }

        // GET: api/Catalog
        [HttpGet]
        public string Get()
        {
            string rs = "";
            using (IEnumerator<CatalogItem> catalogItemEnumerator = _catalogContext.CatalogItems.GetEnumerator())
            {
                while (catalogItemEnumerator.MoveNext())
                {
                    rs += catalogItemEnumerator.Current.Name + " ";
                }
            }
            return rs;
        }
    }
}
