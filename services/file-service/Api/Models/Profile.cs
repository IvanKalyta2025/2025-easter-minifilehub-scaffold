using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Profile
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string LastName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string AvatarUrl { get; set; } = string.Empty;

        public User User { get; set; } = null!;

    }
}
