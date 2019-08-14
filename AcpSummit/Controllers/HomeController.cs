using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AcpSummitApp.Controllers
{
    
    public class HomeController : Controller
    {
        CatalogApi api = new CatalogApi();
        string rs = "";

        public async Task<IActionResult> Index()
        {
            HttpClient client = api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Catalog");
            if (res.IsSuccessStatusCode) {
                var result = res.Content.ReadAsStringAsync().Result;
                rs = JsonConvert.DeserializeObject<string>(result);
            }
            return View();
        }

        // GET: api/Home
        [HttpGet]
        public void Get(string source)
        {
            string resultset = "";
        }
    }
}
