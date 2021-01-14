using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ChessProject.Models
{
    [Table("Contacts")]
    public class ContactInfo
    {
        public int ContactInfoId { get; set; }

        [RegularExpression(@"^07(\d{8})$", ErrorMessage = "This is not a valid phone number!")]
        public string PhoneNumber { get; set; }


        [Required, RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "This is not a valid email!")]
        public string Email { get; set; }


        // one-to-one relationship
        public virtual Producer Producer { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}