using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AcpSummitApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
// this is sarah's comment

namespace AcpSummitApp.Controllers
{
    
    public class HomeController : Controller
    {
      
        public async Task<IActionResult> Index()
        {
            List<CatalogItem> rs = new List<CatalogItem>();
             using (var client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync("https://acpsummit2019api.azurewebsites.net/api/Catalog?source=mock");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    rs = JsonConvert.DeserializeObject<List<CatalogItem>>(result);
                }
            }

            return View(rs);
        }

        // GET: Home/Get
        [HttpGet]
        public string Get()
        {
            return "successful";
        }


        // GET: Home/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CatalogItem catalogItem)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://acpsummit2019api.azurewebsites.net/api/Catalog/create");
                var response = await client.PostAsJsonAsync<CatalogItem>("create", catalogItem);
                bool returnValue = await response.Content.ReadAsAsync<bool>();

                if (returnValue)
                {
                    return RedirectToAction("Index");
                }

            } 
                
            return View(catalogItem);
        }

    }
}
