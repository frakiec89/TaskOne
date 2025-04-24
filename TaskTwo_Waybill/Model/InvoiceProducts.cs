using System.ComponentModel.DataAnnotations;

namespace TaskTwo_Waybill.Model
{
    public class InvoiceProducts
    {
        [Key]
        public int Id { get; set; }
        public  Invoice  Invoice { get; set; }
        public ProductPriceHistory Product {  get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "кол-во должно быть не меньше 0")]
        public int  Count { get; set; }
    }
}
