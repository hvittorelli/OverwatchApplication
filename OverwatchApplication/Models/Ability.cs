using System.Collections.Generic;

namespace OverwatchApplication.Models
{
    public class Ability
    {
        public int AbilityID { get; set; } // PK
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AbilityType Type { get; set; }
        public float Cooldown { get; set; }

        // FK
        public int HeroID { get; set; }

        // Navigation property 
        public Hero? Hero { get; set; }
    }
}

