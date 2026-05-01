using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BookmakerWeb.Models
{
    public class Bet
    {
        public int BetId {get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] //Задает точный тип колонки в бд для денег(18 знаков всего, 2 после запятой)
        public decimal Amount {get; set; }

        [Required]
        public string OutCome {get; set; } = string.Empty;

        public decimal Odds {get; set; } = 1.0m; //Коэфф.

        public DateTime BetDate {get; set; } = DateTime.Now;


         public int UserId {get; set; } //Внешние ключи
         public int MatchId {get; set; }

        //Навигационное свойство в зависимой сущности
         [ForeignKey("UserId")]
         public User? User {get; set; }

         [ForeignKey("MatchId")]
         public Match? Match {get; set; }
    }
}