using System;

namespace API.CreditCard.CreditCard.Models
{
    public class CreditCardDto
    {
        public Guid CardId { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string CVC { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
