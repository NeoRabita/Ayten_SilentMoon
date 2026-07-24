using System;
using System.ComponentModel.DataAnnotations.Schema;
using SlientMoon.Domain.Common;

namespace SlientMoon.Domain.Entities
{
    [Table("RefreshTokens")]
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
    }
}