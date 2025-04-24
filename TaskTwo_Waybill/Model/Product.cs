using System.ComponentModel.DataAnnotations;

namespace TaskTwo_Waybill.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        public string Name { get; set; }

        public string Unit { get; set; } = string.Empty;
    }
}
