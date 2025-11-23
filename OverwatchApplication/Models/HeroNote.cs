using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace OverwatchApplication.Models
{
    public class HeroNote
    {
        [Key]
        public int NoteID { get; set; } // PK
        public string Content { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public float HoursPlayed { get; set; }

        // FKs
        public int HeroID { get; set; }
        public Hero? Hero { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }
    }
}