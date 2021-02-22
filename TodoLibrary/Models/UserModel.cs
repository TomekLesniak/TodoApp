using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoLibrary.Models
{
    /// <summary>
    /// Represents one user.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// The unique identifier for User.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// The first name of user.
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        
        /// <summary>
        /// The last name of the user.
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        /// <summary>
        /// First and Last name combined together.
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
    }
}