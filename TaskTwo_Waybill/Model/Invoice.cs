

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTwo_Waybill.Model
{
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); 
        
        /// <summary>
        /// от
        /// </summary>
        public Supplier From { get; set; }
        
        /// <summary>
        /// кому 
        /// </summary>
        public Supplier To { get; set; }


        [ForeignKey(nameof(Supplier))]
        public int FromId { get; set; }


        [ForeignKey(nameof(Supplier))]
        public int ToId { get; set; }
       

        public DateTime DateTime { get; set; }

    }
}
