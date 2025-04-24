using System.ComponentModel.DataAnnotations;

namespace TaskTwo_Waybill.Model
{
    public class ProductPriceHistory
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Цена должна быть не меньше 0")]
        public decimal Price { get; set; }
        
        public DateTime DateTime { get; set; }
    }
}