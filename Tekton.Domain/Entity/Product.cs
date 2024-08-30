using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Domain.Entity
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }    

        public int StatusId { get; set; }
        public ProductStatus StatusName { get; set; }
        public double Stock { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }

        public double FinalPrice { get { return this.Price * (100 - this.Discount) / 100; } set { } } 


    }
}
