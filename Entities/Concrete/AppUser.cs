using Core.Abstract.Entities;
using Microsoft.AspNetCore.Identity;

namespace Entities.Concrete
{
    public enum Status { Active = 1, Modified = 2, Passive = 3 }

    public class AppUser : IdentityUser, IEntity
    {
        public string? Occupation { get; set; }

        private DateTime _createDate =DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate=value; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }
}
    