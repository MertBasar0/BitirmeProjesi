using Core.Abstract.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Satişlar")]
    public class Sale :IEntity
    {
        public int SaleId { get; set; }

        public decimal Total { get; set; }


        //Fluent API Prop

        public Customer? Customer { get; set; }
    }
}
