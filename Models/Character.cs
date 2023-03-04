using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game_rpg.enums;

namespace game_rpg.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodoo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass rpgClass { get; set; } = RpgClass.Kinght;
    }
}