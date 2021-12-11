using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Result> Results { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<State>().HasIndex(
                        state => new { state.Name }).IsUnique(true);

            modelBuilder.Entity<Candidate>().HasData(new Candidate
            {
                Id = 1,
                Code = "DT",
                FirstName = "Donald",
                LastName = "Trump",
            });

            modelBuilder.Entity<Candidate>().HasData(new Candidate
            {
                Id = 2,
                Code = "HC",
                FirstName = "Hillary",
                LastName = "Clinton",
            });

            modelBuilder.Entity<Candidate>().HasData(new Candidate
            {
                Id = 3,
                Code = "JB",
                FirstName = "Joe",
                LastName = "Biden",
            });

            modelBuilder.Entity<Candidate>().HasData(new Candidate
            {
                Id = 4,
                Code = "JFK",
                FirstName = "John",
                LastName = "F. Kennedy",
            });

            modelBuilder.Entity<Candidate>().HasData(new Candidate
            {
                Id = 5,
                Code = "JR",
                FirstName = "Jack",
                LastName = "Randall",
            });

        }
    }
}
