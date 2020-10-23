using AutoFixture;
using FluentValidation.TestHelper;
using MakersOfDenmark.Application.Commands.V1;
using MakersOfDenmark.Application.Commands.Validators;
using Xunit;

namespace MakersOfDenmark.Application.Tests.Validator
{
    public class AddressValidatorTest
    {
        private Fixture _fixture;
        private AddressValidator _validator;
        public AddressValidatorTest()
        {
            _fixture = new Fixture();
            _validator = new AddressValidator();
        }
        [Fact]
        public void AllFieldsEntered()
        {
            var registerMakerSpace = _fixture.Create<RegisterMakerSpace>();
            var result = _validator.TestValidate(registerMakerSpace);
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact]
        public void CountryMissing()
        {
            var registerMakerSpace = _fixture.Build<RegisterMakerSpace>().Without(x => x.Country).Create(); ;
            var result = _validator.TestValidate(registerMakerSpace);
            result.ShouldHaveValidationErrorFor(x => x.Country);
        }
        [Fact]
        public void CityMissing()
        {
            var registerMakerSpace = _fixture.Build<RegisterMakerSpace>().Without(x => x.City).Create(); ;
            var result = _validator.TestValidate(registerMakerSpace);
            result.ShouldHaveValidationErrorFor(x => x.City);
        }
        [Fact]
        public void StreetMissing()
        {
            var registerMakerSpace = _fixture.Build<RegisterMakerSpace>().Without(x => x.Street).Create(); ;
            var result = _validator.TestValidate(registerMakerSpace);
            result.ShouldHaveValidationErrorFor(x => x.Street);
        }
        [Fact]
        public void PostCodeMissing()
        {
            var registerMakerSpace = _fixture.Build<RegisterMakerSpace>().Without(x => x.PostCode).Create(); ;
            var result = _validator.TestValidate(registerMakerSpace);
            result.ShouldHaveValidationErrorFor(x => x.PostCode);
        }
    }
}
