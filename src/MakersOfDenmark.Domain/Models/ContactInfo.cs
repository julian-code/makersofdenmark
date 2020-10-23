namespace MakersOfDenmark.Domain.Models
{
    public class ContactInfo : Entity<int>
    {
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
