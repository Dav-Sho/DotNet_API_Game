using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace game_rpg.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class Auth: ControllerBase
    {
        private readonly AuthRepository _authRepo;
        public Auth(AuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(RegisterUserDto user) {
            var newUser = new User{Username = user.Username, Email= user.Email};
            return Ok(await _authRepo.Register( newUser,user.password));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(LoginDto user) {

            return Ok(await _authRepo.Login(user.Email, user.password));
        }

        


    }
}