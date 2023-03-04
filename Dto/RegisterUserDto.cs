using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace game_rpg.Dto
{
    public class RegisterUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}