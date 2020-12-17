using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakersOfDenmark.WebAPI.ViewModels
{
    public class EventViewModel
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Description { get; set; }
        public string Badge { get; set; }

    }
}
