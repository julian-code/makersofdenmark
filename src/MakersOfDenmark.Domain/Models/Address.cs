using System.Collections.Generic;

namespace MakersOfDenmark.Domain.Models
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Floor { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string PostCode { get; private set; }
        public string FullAddress => string.IsNullOrEmpty(Street) ? null : $"{Street} {Number}{(string.IsNullOrEmpty(Floor) ? "" : $", {Floor},")} {PostCode} {City}";

        public Address()
        {

        }
        public Address(string street, string number, string city, string country, string postCode, string floor)
        {
            Street = street;
            Number = number;
            City = city;
            Country = country;
            PostCode = postCode;
            Floor = floor;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return Number;
            yield return Floor;
            yield return City;
            yield return Country;
            yield return PostCode;
        }
    }
}
