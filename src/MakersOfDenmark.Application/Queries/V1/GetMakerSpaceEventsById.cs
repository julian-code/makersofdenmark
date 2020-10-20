using System;
using System.Collections.Generic;
using System.Text;

namespace MakersOfDenmark.Application.Queries.V1
{
    public class GetMakerSpaceEventsById
    {
        public Guid Id { get; set; }
    }

    public class GetMakerSpaceEventsByIdResponse
    {
        public IEnumerable<EventViewModel> Events { get; set; }
    }

    public class EventViewModel
    {
        public Guid Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string EventType { get; set; }
        public string Description { get; set; }
    }
}
