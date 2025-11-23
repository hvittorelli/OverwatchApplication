using System.Collections.Generic;
using System;

namespace OverwatchApplication.Models
{
    public class Hero
    {
        public int HeroID { get; set; }   // PK
        public string Name { get; set; } = string.Empty;
        public RoleType Role { get; set; } 
        public string Description { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public string CountryOfOrigin { get; set; } = string.Empty;
        public int DifficultyToMaster { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string WeaponType { get; set; } = string.Empty;

        // Navigation
        public ICollection<Ability> Abilities { get; set; } = new List<Ability>();
        public ICollection<HeroNote> Notes { get; set; } = new List<HeroNote>();
    }
}