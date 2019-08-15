using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcpSummitApi.Helpers;
using AcpSummitApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcpSummitApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : Controller
    {
        public CatalogContextSeed _contextSeed;
        public CatalogContext _catalogContext;
        public CatalogItemsHelper _catalogItemsHelper;

        public CatalogController(IHostingEnvironment hostingEnvironment)
        {
            _contextSeed = new CatalogContextSeed(hostingEnvironment);
            _catalogContext = new CatalogContext();
            _catalogContext.CatalogItems = new List<CatalogItem>();
            _catalogItemsHelper = new CatalogItemsHelper();
        }

        // GET: api/Catalog
        [HttpGet]
        public List<CatalogItem> Get(string source)
        {
            if (!_catalogContext.CatalogItems.Any())
            {
                _contextSeed.SeedCatalogItems(_catalogContext, source);
            }
            return _catalogContext.CatalogItems;
            //return (_catalogItemsHelper.PrintItemsInTabularFormat(_catalogContext.CatalogItems.GetEnumerator()));
        }

        [Route("print")]
        public string Print(string source)
        {
            if (!_catalogContext.CatalogItems.Any())
            {
                _contextSeed.SeedCatalogItems(_catalogContext, source);
            }
            return _catalogItemsHelper.PrintItemsInTabularFormat(_catalogContext.CatalogItems.GetEnumerator());
        }

        /*
        // GET: api/Catalog/Create
        [HttpGet]
        [Route("create")]
        public IActionResult Create(string Name, string Description, string Price)
        {
            _contextSeed.AddCatalogItemDataToCsv(Name,Description,Price);
            return Ok();
        }
        */


        // GET: api/Catalog/Create
        [HttpGet]
        [Route("create")]
        public IActionResult Create(CatalogItem catalogItem)
        {
            _contextSeed.AddCatalogItemToCsv(catalogItem);
            return Ok();
        }
        
    }
}
