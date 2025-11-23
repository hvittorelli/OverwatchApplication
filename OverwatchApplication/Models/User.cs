using System.Collections.Generic;

namespace OverwatchApplication.Models 
{ 
    public class User 
    { 
        public int UserID { get; set; } //PK
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<HeroNote> Notes { get; set; } = new List<HeroNote>(); 
    } 
}