using Entities.Concrete.Dto_s;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("Kullanıcı adınızı yeniden giriniz.");
            RuleFor(x => x.Password).NotNull().WithMessage("Şifrenizi yeniden giriniz.");
        }
    }
}
