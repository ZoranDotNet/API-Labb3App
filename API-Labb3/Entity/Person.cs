using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Labb3.Entity
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Email { get; set; } = null!;

        [Column(TypeName = "varchar(20)")]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        public ICollection<Hobby> Hobbies { get; set; } = new List<Hobby>();
    }
}
