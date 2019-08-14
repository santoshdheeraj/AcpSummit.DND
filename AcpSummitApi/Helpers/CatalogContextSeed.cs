using AcpSummitApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AcpSummitApi.Helpers
{
    public class CatalogContextSeed
    {
        public void SeedCatalogItems(CatalogContext context, string source)
        {
            if (source == "csv")
            {
                //string currentDirectory = System.Environment.CurrentDirectory;
                //string csvFileCatalogItems = Path.Combine(currentDirectory,"Setup", "CatalogItems.csv");
                string csvFileCatalogItems = Path.Combine("Setup", "CatalogItems.csv");

                if (!File.Exists(csvFileCatalogItems))
                    SeedCatalogItems(context, "mock");

                string[] csvheaders = new string[] { };
                try
                {
                    string[] requiredHeaders = { "id", "name", "description", "price" };
                    csvheaders = GetHeaders(csvFileCatalogItems, requiredHeaders);
                }
                catch (Exception ex)
                {
                    GetMockItems();
                }

                var lines = File.ReadAllLines(csvFileCatalogItems).Skip(1).Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"));
                foreach (var line in lines)
                {
                    context.CatalogItems.Add(CreateCatalogItemByValues(line[0], line[1], line[2], Decimal.Parse(line[3])));
                }

            }
            else
            {
                using (IEnumerator<CatalogItem> catalogEnumerator = GetMockItems().GetEnumerator())
                {
                    while (catalogEnumerator.MoveNext())
                    {
                        CatalogItem catItem = CreateCatalogItemByValues(catalogEnumerator.Current.Id, catalogEnumerator.Current.Name, catalogEnumerator.Current.Description, catalogEnumerator.Current.Price);
                        context.CatalogItems.Add(catItem);
                    }
                }
            }

        }

        private void CreateCatalogItemsFromCsv(string[] column, CatalogContext context, string[] headers)
        {
            var catalogItem = CreateCatalogItemByValues(
                column[Array.IndexOf(headers, "id")].Trim('"').Trim(),
                column[Array.IndexOf(headers, "name")].Trim('"').Trim(),
                column[Array.IndexOf(headers, "description")].Trim('"').Trim(),
                Decimal.Parse(column[Array.IndexOf(headers, "price")].Trim('"').Trim())
                );
            context.CatalogItems.Add(catalogItem);
        }

        private CatalogItem CreateCatalogItemByValues(string id, string name, string description, decimal price)
        {
            var catalogItem = new CatalogItem()
            {
                Id = id,
                Name = name,
                Description = description,
                Price = price,
            };

            return catalogItem;
        }

        private IEnumerable<CatalogItem> GetMockItems()
        {
            return new List<CatalogItem>
            {
                new CatalogItem{ Id = Guid.NewGuid().ToString(), Name = "BLT", Description = "Bacon Lettuce Tomato", Price = 6.5M},
                new CatalogItem{ Id = Guid.NewGuid().ToString(), Name = "Chicken Rotisserie", Description = "Chicken Cheese & Lettuce on a Honey Oat Bread", Price = 7M},
                new CatalogItem{ Id = Guid.NewGuid().ToString(), Name = "The Italian", Description = "Salami Cappicola & Pepporoni on a Italian Bread", Price = 6M},
                new CatalogItem{ Id = Guid.NewGuid().ToString(), Name = "Garden Sub", Description = "Veggies Avocado on a Pesto Wrap", Price = 5.5M},
                new CatalogItem{ Id = Guid.NewGuid().ToString(), Name = "Caprese", Description = "Fresh Mozzarella Tomato and Basil on a Ciabatta", Price = 7.5M},
            };
        }

        private string[] GetHeaders(string csvfile, string[] requiredHeaders, string[] optionalHeaders = null)
        {
            string[] csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

            if (csvheaders.Count() < requiredHeaders.Count())
            {
                throw new Exception($"requiredHeader count '{ requiredHeaders.Count()}' is bigger then csv header count '{csvheaders.Count()}' ");
            }

            if (optionalHeaders != null)
            {
                if (csvheaders.Count() > (requiredHeaders.Count() + optionalHeaders.Count()))
                {
                    throw new Exception($"csv header count '{csvheaders.Count()}'  is larger then required '{requiredHeaders.Count()}' and optional '{optionalHeaders.Count()}' headers count");
                }
            }

            foreach (var requiredHeader in requiredHeaders)
            {
                if (!csvheaders.Contains(requiredHeader))
                {
                    throw new Exception($"does not contain required header '{requiredHeader}'");
                }
            }

            return csvheaders;
        }

        public void AddCatalogItemDataToCsv(string Name, string Description, string Price)
        {
            AddCatalogItemToCsv(CreateCatalogItemByValues("0",Name,Description,Decimal.Parse(Price)));
        }

        public void AddCatalogItemToCsv(CatalogItem catalogItem)
        {
            var csv = new StringBuilder();
            var newLine = string.Format("{0},{1},{2},{3}", Guid.NewGuid().ToString(), catalogItem.Name, catalogItem.Description, catalogItem.Price);
            csv.AppendLine(newLine);

            //string currentDirectory = System.Environment.CurrentDirectory;
            //string csvFileCatalogItems = Path.Combine(currentDirectory, "Setup", "CatalogItems.csv");
            string csvFileCatalogItems = Path.Combine("Setup", "CatalogItems.csv");
            File.AppendAllText(csvFileCatalogItems, csv.ToString());
        }
    }
}
