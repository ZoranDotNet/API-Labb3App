using API_Labb3.Entity;

namespace API_Labb3.DTOs
{
    public class HobbyDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public int PersonId { get; set; }

        public ICollection<Link> Links { get; set; } = new List<Link>();
    }
}
