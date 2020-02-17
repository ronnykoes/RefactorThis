using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RefactorThis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var dataContext = new DataContext("Data Source=App_Data/products.db");
            //var productProvider = new ProductProvider(dataContext);
            //var product = productProvider.Get(new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"));
            //Console.WriteLine($"Name: {product.Name} ");
        
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}