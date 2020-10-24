using System;
using System.Collections.Generic;
using System.Text;

namespace MakersOfDenmark.Application.Commands.V1
{
    // TODO:
    // Issue #23
    public class RegisterToMakerSpaceEvent
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
    }
    public class RegisterToMakerSpaceEventResponse
    {
        public bool UserRegistered { get; set; }
    }
}
