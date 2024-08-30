using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Domain.Entity
{
    public class ProductStatus
    {
        public int ProductId { get; set; }
        public int Status { get; set; }
            
        public string StatusName { get; set; }
    }
}
