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
        private readonly Fixture _fixture;
        private readonly BaseMakerSpaceValidator _validator;
        public BaseMakerSpaceValidatorTest()
        {
            _fixture = new Fixture();
            _validator = new BaseMakerSpaceValidator();
        }
        [Fact]
        public void NameAndLogoUrlEntered()
        {
            //Arrange
            var registerMakerSpace = _fixture.Build<RegisterMakerSpace>().With(x => x.LogoUrl, "https://google.com").Create();
            
            //Act
            var result = _validator.TestValidate(registerMakerSpace);
            
            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact]
        public void NameMissing()
        {
            //Arrange
            var registerMakerSpace = _fixture.Build<RegisterMakerSpace>().Without(x=>x.Name).With(x => x.LogoUrl, "https://google.com").Create();
            
            //Act
            var result = _validator.TestValidate(registerMakerSpace);
            
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        [Fact]
        public void LogoUrlMissing()
        {
            //Arrange
            var registerMakerSpace = _fixture.Build<RegisterMakerSpace>().Without(x => x.LogoUrl).Create();
            
            //Act
            var result = _validator.TestValidate(registerMakerSpace);
            
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.LogoUrl);
        }


    }
}
