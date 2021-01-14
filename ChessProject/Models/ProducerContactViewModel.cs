using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChessProject.Models
{
    public class ProducerContactViewModel
    {
        [MinLength(2, ErrorMessage = "Producer name cannot be less than 2!"),
       MaxLength(30, ErrorMessage = "Producer name cannot be more than 30!")]
        public string Name { get; set; }

        [RegularExpression(@"^07(\d{8})$", ErrorMessage = "This is not a valid phone number!")]
        public string PhoneNumber { get; set; }


        [Required, RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "This is not a valid email!")]
        public string Email { get; set; }


    }
}