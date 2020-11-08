using System;
using System.ComponentModel.DataAnnotations;

namespace API.CreditCard.CreditCard.Models
{
    public class CreditCardViewModel
    {
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Name must be alphanumeric" )]
        [MaxLength(50, ErrorMessage = "Name is too long")]
        public string Name { get; set; }
        
        [CreditCard(ErrorMessage = "CardNumber is not valid")]
        public string CardNumber { get; set; }
        
        public int CVC { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
