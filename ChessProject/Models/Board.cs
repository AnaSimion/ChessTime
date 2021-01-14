using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChessProject.Models.MyValidation;


namespace ChessProject.Models
{
    public class Board
    {

        public int BoardId { get; set; }

        [MinLength(2, ErrorMessage = "Name cannot be less than 2!"),
            MaxLength(200, ErrorMessage = "Name cannot be more than 200!")]
        public string Name { get; set; }

        [MinLength(3, ErrorMessage = "Owner cannot be less than 3!"),
        MaxLength(50, ErrorMessage = "Owner cannot be more than 50!")]
        public string Owner { get; set; }


        [MinLength(2, ErrorMessage = "Description cannot be less than 2!"),
        MaxLength(5000, ErrorMessage = "Description cannot be more than 5000!")]
        public string Description { get; set; }

        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Price can't be negative"), PalindromeNumberValidator]
        public int Price { get; set; }

        [RegularExpression(@"^[12]\d{3,3}$", ErrorMessage = "Year needs to start with 1 or 2 and be 4 digits long")]
        public int Year { get; set; }

        // one-to-many relationship
        public int ProducerId { get; set; }
        public virtual Producer Producer { get; set; }

        // one-to-many relationship
        [ForeignKey("BoardType")]
        public int BoardTypeId { get; set; }
        public virtual BoardType BoardType { get; set; }


        // many-to-many relationship
        public virtual ICollection<Material> Materials { get; set; }

        // dropdown lists
        public IEnumerable<SelectListItem> BoardTypeList { get; set; }
        public IEnumerable<SelectListItem> ProducerList { get; set; }

        // checkboxes list
        [NotMapped]
        public List<CheckBoxViewModel> MaterialsList { get; set; }
    }
}
