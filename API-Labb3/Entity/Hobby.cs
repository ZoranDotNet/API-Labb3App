using System.ComponentModel.DataAnnotations;

namespace API_Labb3.Entity
{
    public class Hobby
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(300)]
        public string Description { get; set; } = null!;

        public int PersonId { get; set; }

        public ICollection<Link> Links { get; set; } = new List<Link>();
    }
}
