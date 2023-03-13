using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace game_rpg.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FightController: ControllerBase
    {
        private readonly FightService _fight;
        public FightController(FightService fight)
        {
            _fight = fight; 
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> WeaponAttack(WeaponAttackDto weaponAttack) {
            return Ok(await _fight.WeaponAttack(weaponAttack));
        }
    }
}