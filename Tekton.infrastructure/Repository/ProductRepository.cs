using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tekton.Application.Interfaces;
using Tekton.Domain.Entity;
using Tekton.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Reflection.Emit;

namespace Tekton.infrastructure.Repository
{

    public class ProductRepository : IProductRepository
    {
        private readonly TektonContext _context;
        protected readonly IConfiguration Configuration;
        private List<Product> products;
        public ProductRepository(TektonContext context, IConfiguration configuration)
        {
            this._context = context;
            this.Configuration = configuration;
            this.products = connectAPIMock();
        }
        private List<Product> connectAPIMock()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(Configuration["UrlMock"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(Configuration["UrlMock"]).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseJson = response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(responseJson.Result);
            }
            return products;
        }
        public IEnumerable<Product> GetAll(params Expression<Func<Product, object>>[] includes)
        {
           
            var query = _context.Set<Product>().AsQueryable();
            foreach (Expression<Func<Product, object>> i in includes)
            {
                
                query = query.Include(i);
            }
            foreach (var item in query)
            {
                item.Discount = products.FirstOrDefault(x => x.ProductId == item.ProductId).Discount;
            }
            return query.ToList();
        }
       public  IEnumerable<Product> Filter(Expression<Func<Product, bool>> where, params Expression<Func<Product, object>>[] includes)
        {
            var query = _context.Set<Product>().Where(where);
            foreach (Expression<Func<Product, object>> i in includes)
            {
                query = query.Include(i);
            }
            foreach (var item in query)
            {
                item.Discount = products.FirstOrDefault(x => x.ProductId == item.ProductId).Discount;
            }
            return query.ToList();
        }

        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
