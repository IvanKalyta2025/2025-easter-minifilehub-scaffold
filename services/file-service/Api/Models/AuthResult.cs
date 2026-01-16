using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class AuthResult
    {
        public record AuthResult(bool Success, string Message);
    }
}