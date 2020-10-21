using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MakersOfDenmark.Application.Commands.V1
{
    public class EditBaseMakerSpace : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AddressStreet { get; set; }
        public string AddressPostCode { get; set; }
        public string AddressCountry { get; set; }
        public string AddressCity { get; set; }
        public string ContactInfoPhone { get; set; }
        public string ContactInfoEmail { get; set; }
        public string VATNumber { get; set; }
        public string LogoUrl { get; set; }
        public string AccessType { get; set; }
    }
}
