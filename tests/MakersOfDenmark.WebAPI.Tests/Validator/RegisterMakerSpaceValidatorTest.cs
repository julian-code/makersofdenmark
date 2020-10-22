using AutoFixture;
using FluentValidation.TestHelper;
using MakersOfDenmark.Application.Commands.V1;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MakersOfDenmark.WebAPI.Tests.Validator
{
    public class RegisterMakerSpaceValidatorTest : IClassFixture<RequestHandlerFixture>
    {
        private readonly RequestHandlerFixture _rhf;
        private RegisterMakerSpaceValidator _registerMakerSpaceValidator;
        public RegisterMakerSpaceValidatorTest(RequestHandlerFixture rhf)
        {
            _registerMakerSpaceValidator = new RegisterMakerSpaceValidator();
            _rhf = rhf;
        }

        [Fact]
        public void ValidName()
        {
            RegisterMakerSpace model = _rhf.Fixture.Build<RegisterMakerSpace>().Without(x =>x.Name).Create();
            var result = _registerMakerSpaceValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}
