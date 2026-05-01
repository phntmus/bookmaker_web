using Microsoft.EntityFrameworkCore;
using BookmakerWeb.Models;

namespace BookmakerWeb.Data
{
    public class BookmakerDbContext : DbContext
    {
        public BookmakerDbContext() { }

        public BookmakerDbContext(DbContextOptions<BookmakerDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Bet> Bets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Bet>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bets)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bet>()
                .HasOne(b => b.Match)
                .WithMany(m => m.Bets)
                .HasForeignKey(b => b.MatchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Тестовые данные
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FullName = "Иванов Иван Петрович",
                    Email = "ivanov@mail.ru",
                    PhoneNumber = "+79001234567",
                    RegDate = new DateTime(2024, 1, 15)
                },
                new User
                {
                    UserId = 2,
                    FullName = "Петрова Анна Сергеевна",
                    Email = "petrova@mail.ru",
                    PhoneNumber = "+79007654321",
                    RegDate = new DateTime(2024, 2, 20)
                },
                new User
                {
                    UserId = 3,
                    FullName = "Сидоров Алексей Михайлович",
                    Email = "sidorov@mail.ru",
                    PhoneNumber = "+79009876543",
                    RegDate = new DateTime(2024, 3, 10)
                });

            // Матчи/События
            modelBuilder.Entity<Match>().HasData(
                new Match
                {
                    MatchId = 1,
                    EventName = "Реал Мадрид — Барселона",
                    StartTime = new DateTime(2024, 6, 15, 20, 0, 0),
                    Sport = "Футбол"
                },
                new Match
                {
                    MatchId = 2,
                    EventName = "ЦСКА — Спартак",
                    StartTime = new DateTime(2024, 6, 16, 18, 30, 0),
                    Sport = "Футбол"
                },
                new Match
                {
                    MatchId = 3,
                    EventName = "Лейкерс — Голден Стэйт",
                    StartTime = new DateTime(2024, 6, 17, 3, 0, 0),
                    Sport = "Баскетбол"
                });

            // Ставки (минимум 3 по ТЗ)
            modelBuilder.Entity<Bet>().HasData(
                new Bet
                {
                    BetId = 1,
                    Amount = 1000,
                    OutCome = "Победа Реала",
                    Odds = 2.10m,
                    BetDate = new DateTime(2024, 6, 14, 15, 30, 0),
                    UserId = 1,
                    MatchId = 1
                },
                new Bet
                {
                    BetId = 2,
                    Amount = 500,
                    OutCome = "Тотал больше 2.5",
                    Odds = 1.85m,
                    BetDate = new DateTime(2024, 6, 15, 10, 0, 0),
                    UserId = 1,
                    MatchId = 2
                },
                new Bet
                {
                    BetId = 3,
                    Amount = 2000,
                    OutCome = "Победа Lakers",
                    Odds = 1.75m,
                    BetDate = new DateTime(2024, 6, 16, 20, 0, 0),
                    UserId = 2,
                    MatchId = 3
                },
                new Bet
                {
                    BetId = 4,
                    Amount = 1500,
                    OutCome = "Ничья",
                    Odds = 3.20m,
                    BetDate = new DateTime(2024, 6, 15, 12, 0, 0),
                    UserId = 3,
                    MatchId = 1
                });
        }
    }
}