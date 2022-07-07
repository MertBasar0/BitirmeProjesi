using Core.Abstract.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("SepetUrunEslesme")]
    public class BasketProduct :IEntity
    {
        //CompositeKey Table

        public int? ProductId { get; set; }

        public Product? Product { get; set; }

        public int? BasketId { get; set; }

        public Basket? Basket { get; set; }
    }
}
