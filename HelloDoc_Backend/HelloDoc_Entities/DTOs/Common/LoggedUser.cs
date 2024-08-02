using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloDoc_Entities.DTOs.Common
{
    public class LoggedUser
    {
        public long UserId { get; set; }

        public int Role { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}