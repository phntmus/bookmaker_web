using System.ComponentModel.DataAnnotations;


namespace BookmakerWeb.Models
{
    public class User
    {
        [Key]
        public int UserId {get; set; }

        [Required]
        [StringLength(100)]
        public string FullName {get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Email {get; set; } = string.Empty;

        [StringLength(20)]
        public string PhoneNumber {get; set; } = string.Empty;

        [StringLength(20)]
        public DateTime RegDate {get; set; } = DateTime.Now;

        //Навигационное свойство(главная сущность)
        public ICollection<Bet> Bets {get; set; } = new List<Bet>();
    }

}