using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Tekton.Domain.Entity;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Tekton.Infrastructure
{

        public class TektonContext : DbContext
        {
            protected readonly IConfiguration Configuration;

            public TektonContext(DbContextOptions<TektonContext> options, IConfiguration configuration)
                : base(options)
            {
                Configuration = configuration;
            }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

             HttpClient client = new HttpClient();
            List<Product> product;
            client.BaseAddress = new Uri(Configuration["UrlMock"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response =  client.GetAsync(Configuration["UrlMock"]).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseJson = response.Content.ReadAsStringAsync();
                product= JsonConvert.DeserializeObject<List<Product>>(responseJson.Result);
                modelBuilder.Entity<Product>().HasData(product);
            }
            else { 

            modelBuilder.Entity<Product>().HasData(
           new Product {ProductId = 1,Name = "Product 1", Description = "Product 1 Desc", Stock=10,Price=100},
           new Product { ProductId = 2, Name = "Product 2", Description = "Product 2 Desc", Stock = 2, Price = 234 },
           new Product { ProductId = 3, Name = "Product 3", Description = "Product 3 Desc", Stock = 4, Price = 400 },
           new Product { ProductId = 4, Name = "Product 4", Description = "Product 4 Desc", Stock = 5, Price = 550 },
           new Product { ProductId = 5, Name = "Product 5", Description = "Product 5 Desc", Stock = 1, Price = 43 }
);
            }
            base.OnModelCreating(modelBuilder);

            }
        }
    
}