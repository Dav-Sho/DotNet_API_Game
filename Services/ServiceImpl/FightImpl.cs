using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace game_rpg.Services.ServiceImpl
{
    public class FightImpl:FightService
    {
        private readonly DataContext _context;
        public FightImpl(DataContext context)
        {
            _context = context;
            
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto weaponAttack)
        {
            var response = new ServiceResponse<AttackResultDto>();

            var attacker = await _context.Characters.Include(c => c.Skills)
            .FirstOrDefaultAsync(c => c.Id == weaponAttack.AttackerId);

            var opponent = await _context.Characters.FirstOrDefaultAsync(c => c.Id == weaponAttack.OpponentId);

            System.Console.WriteLine("Attacker", attacker);
            System.Console.WriteLine("Opponent", opponent);

            if(attacker is null ||  opponent is null || attacker.Weapon is null) {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Something fishy is going on";
                return response;
            }

            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
            damage = new Random().Next(opponent.Defeats);

            if(damage > 0) 
                opponent.HitPoints -= damage;

            if(opponent.HitPoints <=0) {
                response.Message = $"{opponent.Name} has been defeated";
            }

            await _context.SaveChangesAsync();
            response.Data = new AttackResultDto{
                Attacker = attacker.Name,
                Opponent = opponent.Name,
                AttacketHP = attacker.HitPoints,
                OpponentHp = opponent.HitPoints,
                Damage = damage
            };

            return response;

        
        }
    }
}