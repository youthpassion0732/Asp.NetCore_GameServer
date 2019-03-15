using Microsoft.EntityFrameworkCore;
using DomainEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DAL
{
    public class ApiDbContext : IdentityDbContext<IdentityUser>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        
        public DbSet<Offer> Offer { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Icon> Icon { get; set; }
        public DbSet<QuizCategory> QuizCategory { get; set; }
        public DbSet<QuizOffer> QuizOffer { get; set; }
        public DbSet<GameUser> GameUser { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<QuizHistory> QuizHistory { get; set; }
        public DbSet<QuizSummary> QuizSummary { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
