using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Profile
    {
        public Guid UserId { get; set; }
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string AvatarUrl { get; set; }

        public User User { get; set; } = null!;

    }
}