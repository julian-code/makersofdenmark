using System.Collections.Generic;

namespace MakersOfDenmark.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public ICollection<Tool> Tools { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}