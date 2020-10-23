using AutoFixture;
using FluentValidation.TestHelper;
using MakersOfDenmark.Application.Commands.V1;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MakersOfDenmark.WebAPI.Tests.Validator
{
    public class RegisterMakerSpaceValidatorTest
    {
        private Fixture _fixture;
        private RegisterMakerSpaceValidator _registerMakerSpaceValidator;
        public RegisterMakerSpaceValidatorTest()
        {
            _registerMakerSpaceValidator = new RegisterMakerSpaceValidator();
            _fixture = new Fixture();
        }
        [Fact]
        public void ValidModel__Fixture()
        {
            RegisterMakerSpace model = _fixture.Build<RegisterMakerSpace>().With(x => x.LogoUrl, "https://localhost").Create();
            var result = _registerMakerSpaceValidator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// For illistrative purposes
        /// </summary>
        [Fact]
        public void ValidName__WithoutFixture()
        {
            RegisterMakerSpace model = new RegisterMakerSpace {
                AddressCity = "abc",
                AddressCountry = "def",
                AddressPostCode = "123",
                AddressStreet = "streetgade",
                AccessType = Domain.Enums.AccessType.Private,
                ContactInfoEmail = "hest@abc.dk",
                ContactInfoPhone = "456",
            };
            var result = _registerMakerSpaceValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        [Fact]
        public void ValidName_Fixture()
        {
            RegisterMakerSpace model = _fixture.Build<RegisterMakerSpace>().Without(x => x.Name).Create();
            var result = _registerMakerSpaceValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}
