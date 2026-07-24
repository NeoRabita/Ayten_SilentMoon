using OnionArchitecture.Domain.Common;
using System.Collections.Generic;

namespace OnionArchitecture.Domain.Entities
{
    public class ApplicationUser : BaseEntity
    {
        public ApplicationUser()
        {
            Pomodoros = new HashSet<Pomodoro>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? RefreshTokenId { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public virtual ICollection<Pomodoro> Pomodoros { get; set; }
    }

}
