using System.Collections.Generic;
using System.Threading.Tasks;
using BookmakerWeb.Models;

namespace BookmakerWeb.Services
{
    public interface IBetService
    {
        Task<List<BetDto>> GetAllBetsAsync();
        Task<Bet?> GetBetByIdAsync(int id);
    }
}