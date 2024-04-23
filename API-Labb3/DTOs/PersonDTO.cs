using API_Labb3.Entity;

namespace API_Labb3.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public ICollection<Hobby> Hobbies { get; set; } = new List<Hobby>();

    }
}
