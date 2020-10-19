using System.Collections.Generic;

namespace MakersOfDenmark.Domain.Models
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string PostCode { get; private set; }

        public Address()
        {

        }
        public Address(string street, string city, string country, string postCode)
        {
            Street = street;
            City = city;
            Country = country;
            PostCode = postCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return Country;
            yield return PostCode;
        }
    }
}
