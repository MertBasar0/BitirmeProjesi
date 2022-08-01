using Core.Abstract.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Musteriler")]
    public class Customer : IEntity
    {
        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }


        //Navigation Props

        public virtual Basket? Basket { get; set; }

        //Fluent API props

        public IEnumerable<Sale>? Sales { get; set; }


        public override string ToString()
        {
            return $"{CustomerId}  {CustomerName}";
        }

    }
}
