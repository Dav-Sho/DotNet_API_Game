using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace game_rpg.Dto
{
    public class AddCharacterDto
    {
        public string Name { get; set; } = string.Empty;
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass rpgClass { get; set; } = RpgClass.Kinght;
    }
}