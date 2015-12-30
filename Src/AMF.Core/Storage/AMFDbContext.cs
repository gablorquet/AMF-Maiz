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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>()
           .HasMany(p => p.Prerequisites)
           .WithMany()
           .Map(m =>
           {
               m.MapLeftKey("parent_id");
               m.MapRightKey("child_id");
               m.ToTable("skill_prerequisites");
           });
        }
    }
}
