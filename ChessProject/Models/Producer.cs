using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChessProject.Models
{
    public class Producer
    {
        public int ProducerId { get; set; }

        [MinLength(2, ErrorMessage = "Producer name cannot be less than 2!"),
        MaxLength(30, ErrorMessage = "Producer name cannot be more than 30!")]
        public string Name { get; set; }

        // many-to-one relationship
        public virtual ICollection<Board> Boards { get; set; }

        // one-to one-relationship
        [Required]
        public virtual ContactInfo ContactInfo { get; set; }
    }
}