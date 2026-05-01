using System.ComponentModel.DataAnnotations;

namespace BookmakerWeb.Models
{
    public class Match
    {
        [Key]
        public int MatchId {get; set; }

        [Required]
        [StringLength(200)]
        public string EventName {get; set; } = string.Empty;

        [Required]
        public DateTime StartTime {get; set; }

        [StringLength(100)]
        public string Sport {get; set; } = string.Empty;

        public ICollection<Bet> Bets {get; set; } = new List<Bet>();
    }
}