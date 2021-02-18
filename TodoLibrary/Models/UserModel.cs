using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoLibrary.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}