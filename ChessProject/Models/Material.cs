using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChessProject.Models
{
    public class Material
    {
        public int MaterialId { get; set; }

        [MinLength(3, ErrorMessage = "Material name cannot be less than 3!"),
        MaxLength(30, ErrorMessage = "Material name cannot be more than 30!")]
        public string Name { get; set; }

        // many-to-many relationship
        public virtual ICollection<Board> boards { get; set; }
    }
}