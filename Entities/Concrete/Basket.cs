using Core.Abstract.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Sepetler")]
    public class Basket :IEntity
    {
        public int BasketId { get; set; }

        public DateTime InitTime { get; set; } = DateTime.Now;

        [ForeignKey("musteriNo")]
        public int? CustomerID { get; set; }


        //Navigation Props

        public virtual Customer? Customer { get; set; }
        
        //Fluent API props

        public IEnumerable<BasketProduct>? BasketProducts { get; set; }
    }
}
