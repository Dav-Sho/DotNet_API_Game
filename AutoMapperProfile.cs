using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace game_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Character, GetCharacterDto>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}