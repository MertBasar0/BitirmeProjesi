using Core.Abstract.Entities;
using System.ComponentModel.DataAnnotations.Schema;

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
