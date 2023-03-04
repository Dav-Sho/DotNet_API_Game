using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace game_rpg.Dto
{
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}