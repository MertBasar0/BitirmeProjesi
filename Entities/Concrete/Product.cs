using Core.Abstract.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Urunler")]
    public class Product : IEntity
    {

        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public decimal? UnitPrice { get; set; }



        //Fluent API Props

        public IEnumerable<BasketProduct>? BasketProducts { get; set; }


        public override string ToString()
        {
            return $"{ProductName} {UnitPrice}";
        }
    }
}
