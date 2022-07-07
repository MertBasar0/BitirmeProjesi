using Entities.Concrete.Dto_s;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x=>x.UserName).NotNull().WithMessage("Kullanıcı adı boş geçilemez");
            RuleFor(x => x.Mail).NotNull().Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("mail adresinizi yeniden giriniz.");
        }
        //.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$").WithMessage("parolayı tekrar giriniz");
    }
}
