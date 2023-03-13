using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace game_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WeaponController:ControllerBase
    {
        private readonly WeaponService _weaponService;
        public WeaponController(WeaponService weaponService)
        {
            _weaponService = weaponService;
            
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon(AddWeaponDto addWeaponDto) {
            return Ok(await _weaponService.AddWeapon(addWeaponDto));
        }
    }
}