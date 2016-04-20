using System.Data.Entity;
using AMF.Core.Model;

namespace AMF.Core.Storage
{
    public class AMFDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Animateur> Animateurs { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Nature> Natures { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Spell> Spells { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Legacy> Legacies { get; set; }
        public DbSet<LegacySkill> LegacySkills { get; set; }
        public DbSet<LegacyTree> LegacyTrees { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<SkillBonus> SkillBonuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>()
                .HasMany(x => x.Prerequisites)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("Skill_Id");
                    x.MapRightKey("Prereq_Id");
                    x.ToTable("Skill_Prereq");
                });

            modelBuilder.Entity<LegacySkill>()
                .HasMany(x => x.Prerequisites)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("Legacy_Skill_Id");
                    x.MapRightKey("Legacy_Prereq_Id");
                    x.ToTable("Legacy_Skill_Prereq");
                });

            modelBuilder.Entity<Character>()
                .HasMany(x => x.Categories)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("Character_Id");
                    x.MapRightKey("Category_Id");
                    x.ToTable("Category_Character");
                });

            modelBuilder.Entity<Legacy>()
                .HasMany(x => x.LegacyAvailable)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("Legacy_Id");
                    x.MapRightKey("LegacyTree_Id");
                    x.ToTable("Legacy_Legacy_Tree");
                });

            modelBuilder.Entity<Legacy>()
                .HasMany(x => x.LegacySkills)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("Legacy_Id");
                    x.MapRightKey("LegacySkill_Id");
                    x.ToTable("Legacy_Legacy_Skill");
                });


            modelBuilder.Entity<Character>()
                .HasOptional(x => x.Legacy);

            modelBuilder.Entity<Year>()
                .HasMany(x => x.Events);

        }
    }
}
