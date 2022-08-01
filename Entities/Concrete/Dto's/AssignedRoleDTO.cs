using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dto_s
{
    public class AssignedRoleDTO
    {
        public IdentityRole? Role { get; set; }

        public IEnumerable<AppUser>? HasRoles { get; set; }

        public IEnumerable<AppUser>? HasNotRoles { get; set; }

        public string? RoleName { get; set; }


        public string[]? AddIds { get; set; }
        public string[]? DeletedIds { get; set; }
    }
}
