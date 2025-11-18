using Microsoft.EntityFrameworkCore;
using System;

namespace OverwatchApplication.Models
{
    public class OverwatchContext : DbContext
    {
        public OverwatchContext(DbContextOptions<OverwatchContext> options)
            : base(options) { }

        public DbSet<Hero> Heroes { get; set; } = null!;
        public DbSet<Ability> Abilities { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<HeroNote> HeroNotes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Heroes
            modelBuilder.Entity<Hero>().HasData(
                new Hero
                {
                    HeroID = 1,
                    Name = "Tracer",
                    Role = RoleType.Damage,
                    Description = "Time-jumping, high-mobility damage hero.",
                    ImageURL = "/images/tracer.png",
                    CountryOfOrigin = "United Kingdom",
                    DifficultyToMaster = 3,
                    Gender = "Female",
                    WeaponType = "Pulse Pistols"
                },
                new Hero
                {
                    HeroID = 2,
                    Name = "Reinhardt",
                    Role = RoleType.Tank,
                    Description = "Armored knight with a massive barrier shield.",
                    ImageURL = "/images/reinhardt.png",
                    CountryOfOrigin = "Germany",
                    DifficultyToMaster = 2,
                    Gender = "Male",
                    WeaponType = "Rocket Hammer"
                }
            );

            // Seed Abilities
            modelBuilder.Entity<Ability>().HasData(
                new Ability
                {
                    AbilityID = 1,
                    Name = "Pulse Pistols",
                    Description = "Twin automatic pistols with rapid fire.",
                    Type = AbilityType.PrimaryFire,
                    Cooldown = 0,
                    HeroID = 1
                },
                new Ability
                {
                    AbilityID = 2,
                    Name = "Blink",
                    Description = "Teleport a short distance in the direction you're moving.",
                    Type = AbilityType.Ability1,
                    Cooldown = 3,
                    HeroID = 1
                },
                new Ability
                {
                    AbilityID = 3,
                    Name = "Rocket Hammer",
                    Description = "A large melee weapon that deals sweeping melee damage.",
                    Type = AbilityType.PrimaryFire,
                    Cooldown = 0,
                    HeroID = 2
                },
                new Ability
                {
                    AbilityID = 4,
                    Name = "Barrier Field",
                    Description = "Hold a large energy shield to protect allies.",
                    Type = AbilityType.Ability1,
                    Cooldown = 0,
                    HeroID = 2
                }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserID = 1,
                    UserName = "PlayerOne",
                    Email = "playerone@example.com",
                    PasswordHash = "hashedpassword123"
                }
            );

            // Seed HeroNotes
            modelBuilder.Entity<HeroNote>().HasData(
                new HeroNote
                {
                    NoteID = 1,
                    HeroID = 1,
                    UserID = 1,
                    Content = "Practice aiming while blinking.",
                    DateCreated = new DateTime(2024, 1, 1),
                    HoursPlayed = 5
                },
                new HeroNote
                {
                    NoteID = 2,
                    HeroID = 2,
                    UserID = 1,
                    Content = "Use Barrier Field to protect the team when pushing choke points.",
                    DateCreated = new DateTime(2024, 1, 1),
                    HoursPlayed = 3
                }
            );
        }
    }
}
