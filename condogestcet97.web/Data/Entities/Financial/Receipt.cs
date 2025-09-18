using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Data.Entities.Financial
{
    public enum ReceiptType
    {
        Incoming, 
        Outgoing  
    }
    public class Receipt : IEntity
    {
        public int Id { get; set; }
        public Payment? Payment { get; set; }

        public int PaymentId { get; set; }

        [Column(TypeName = "varchar(50)")]
        [MaxLength(250)]
        public string PayeeName { get; set; }

        [Column(TypeName = "varchar(20)")]
        [MaxLength(250)]
        public string FiscalNumber {  get; set; }

        public ReceiptType Type { get; set; }
    }
}
