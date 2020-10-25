using AutoFixture;
using FluentValidation.TestHelper;
using MakersOfDenmark.Application.Commands.V1;
using MakersOfDenmark.Application.Commands.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MakersOfDenmark.Application.Tests.Validator
{
    public class BaseMakerSpaceValidatorTest
    {
        private Fixture _fixture;
        private BaseMakerSpaceValidator _validator;
        public BaseMakerSpaceValidatorTest()
        {
            _fixture = new Fixture();
            _validator = new BaseMakerSpaceValidator();
        }
        [Fact]
        public void NameAndLogoUrlEntered()
        {
            var registerMakerSpace = _fixture.Build<RegisterMakerSpace>().With(x => x.LogoUrl, "https://google.com").Create();
            var result = _validator.TestValidate(registerMakerSpace);
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact]
        public void NameMissing()
        {
            var registerMakerSpace = _fixture.Build<RegisterMakerSpace>().Without(x=>x.Name).With(x => x.LogoUrl, "https://google.com").Create();
            var result = _validator.TestValidate(registerMakerSpace);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        [Fact]
        public void LogoUrlMissing()
        {
            var registerMakerSpace = _fixture.Build<RegisterMakerSpace>().Without(x => x.LogoUrl).Create();
            var result = _validator.TestValidate(registerMakerSpace);
            result.ShouldHaveValidationErrorFor(x => x.LogoUrl);
        }


    }
}
