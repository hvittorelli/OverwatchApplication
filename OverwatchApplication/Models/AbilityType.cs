using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OverwatchApplication.Models
{
    public enum AbilityType
    {
        [Display(Name = "Primary Fire")]
        PrimaryFire,

        [Display(Name = "Secondary Fire")]
        SecondaryFire,

        [Display(Name = "Ability 1")]
        Ability1,

        [Display(Name = "Ability 2")]
        Ability2,

        [Display(Name = "Passive Ability")]
        Passive,

        [Display(Name = "Ultimate Ability")]
        Ultimate
    }
}

