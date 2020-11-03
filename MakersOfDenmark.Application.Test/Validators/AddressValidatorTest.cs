using AutoFixture;
using FluentValidation.TestHelper;
using MakersOfDenmark.Application.Commands.V1;
using MakersOfDenmark.Application.Commands.V1.admin;
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
            //Arrange
            var registerMakerSpace = _fixture.Create<RegisterMakerSpace>();

            //Act
            var result = _validator.TestValidate(registerMakerSpace);
            
            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact]
        public void CountryMissing()
        {
            //Arrange
            var editMakerSpaceAddress = _fixture.Build<EditMakerSpaceAddress>().Without(x => x.Country).Create(); ;
            
            //Act
            var result = _validator.TestValidate(editMakerSpaceAddress);
            
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Country);
        }
        [Fact]
        public void CityMissing()
        {
            //Arrange
            var registerMakerSpace = _fixture.Build<RegisterMakerSpace>().Without(x => x.City).Create(); ;
            
            //Act
            var result = _validator.TestValidate(registerMakerSpace);
            
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.City);
        }
        [Fact]
        public void StreetMissing()
        {
            //Arrange
            var registerMakerSpace = _fixture.Build<RegisterMakerSpace>().Without(x => x.Street).Create(); ;
            
            //Act
            var result = _validator.TestValidate(registerMakerSpace);
            
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Street);
        }
        [Fact]
        public void PostCodeMissing()
        {
            //Arrange
            var registerMakerSpace = _fixture.Build<RegisterMakerSpace>().Without(x => x.PostCode).Create(); ;
            
            //Act
            var result = _validator.TestValidate(registerMakerSpace);
            
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.PostCode);
        }
    }
}
