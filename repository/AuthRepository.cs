using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace game_rpg.repository
{
    public interface AuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<bool> UserExist(string email);
    }
}