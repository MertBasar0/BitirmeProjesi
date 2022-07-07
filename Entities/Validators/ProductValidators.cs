using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Validators
{
    public class ProductValidators : AbstractValidator<Product>
    {
        public ProductValidators()
        {
            RuleFor(x => x.ProductName).NotNull().WithMessage("Lütfen bir değer giriniz..");
            RuleFor(x => x.UnitPrice).NotNull().WithMessage("Lütfen bir değer giriniz..");
            RuleFor(x => x.UnitsInStock).NotNull().WithMessage("Lütfen bir değer giriniz..");

        }
    }
}

//Kullanıcı Kayıt  oluktan sonra kullanıcıya sitemize hoşgeldiniz şekilnde bir mail gönderin adresine ..
//smtp sınıfını kullancaksın . 