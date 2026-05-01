namespace BookmakerWeb.Models
{
    public class BetDto
    {
        public int BetId { get; set; }
        public decimal Amount { get; set; }
        public string OutCome { get; set; } = string.Empty;
        public decimal Odds { get; set; }
        public DateTime BetDate { get; set; }

        public int UserId { get; set; }
        public int MatchId { get; set; }

        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? MatchName { get; set; }
        public string? MatchSport { get; set; }
    }
}