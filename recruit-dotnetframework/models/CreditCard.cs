using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace recruit_dotnetframework.Models
{
    public class CreditCard
    {
        [Required]
        public string CardNumber { get; set; } 

        [Required]
        public string CVC { get; set; }

        [Required]
        public int ExpiryMonth { get; set; }

        [Required]
        public int ExpiryYear { get; set; }

    }
}