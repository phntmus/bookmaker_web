using Microsoft.EntityFrameworkCore;
using BookmakerWeb.Data;
using BookmakerWeb.Models;

namespace BookmakerWeb.Services
{
    // Реализация сервиса для работы со ставками
    public class BetService : IBetService
    {
        private readonly BookmakerDbContext _context;
        // Конструктор с внедрением зависимости (Dependency Injection)
        public BetService(BookmakerDbContext context)
        {
            _context = context;
        }

        public async Task<List<BetDto>> GetAllBetsAsync()
        {
            return await _context.Bets
                .AsNoTracking()
                .Select(b => new BetDto
                {
                    BetId = b.BetId,
                    Amount = b.Amount,
                    OutCome = b.OutCome,
                    Odds = b.Odds,
                    BetDate = b.BetDate,
                    UserId = b.UserId,
                    MatchId = b.MatchId,
                    UserName = b.User != null ? b.User.FullName : null,
                    UserEmail = b.User != null ? b.User.Email : null,
                    MatchName = b.Match != null ? b.Match.EventName : null,
                    MatchSport = b.Match != null ? b.Match.Sport : null
                })
                .ToListAsync();
        }

        public async Task<Bet?> GetBetByIdAsync(int id)
        {
            return await _context.Bets
                .Include(b => b.User)
                .Include(b => b.Match)
                .FirstOrDefaultAsync(b => b.BetId == id);
        }
    }
}