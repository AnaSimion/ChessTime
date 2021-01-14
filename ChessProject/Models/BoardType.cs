using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChessProject.Models
{
    public class BoardType
    {
        public int BoardTypeId { get; set; }

        [MinLength(2, ErrorMessage = "Board type name cannot be less than 2!"),
        MaxLength(20, ErrorMessage = "Board type name cannot be more than 20!")]
        public string Name { get; set; }

        // one to many
        public virtual ICollection<Board> Boards { get; set; }
    }
}