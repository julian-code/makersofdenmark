using AutoFixture;
using FluentValidation.TestHelper;
using MakersOfDenmark.Application.Commands.V1.admin;
using MakersOfDenmark.Application.Commands.Validators;
using MakersOfDenmark.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.Application.Tests.Validator
{
    public class MakerSpaceIdentifierValidatorTest : IClassFixture<ValidatorFixture>
    {
        private readonly ValidatorFixture _fixture;
        private MakerSpaceIdentifierValidator _validator;
        public MakerSpaceIdentifierValidatorTest(ValidatorFixture fixture)
        {
            _fixture = fixture;
            _validator = new MakerSpaceIdentifierValidator(_fixture.DbContext);
        }
        [Fact]
        public async Task MakerSpaceIdExists()
        {
            //Arrange
            var makerSpace = new MakerSpace { Id = Guid.NewGuid() };
            _fixture.DbContext.MakerSpace.Add(makerSpace);
            await _fixture.DbContext.SaveChangesAsync();
            var editMakerSpaceAddress = _fixture.Fixture.Build<EditMakerSpaceAddress>().With(x => x.MakerSpaceId, makerSpace.Id).Create();

            //Act
            var result = _validator.TestValidate(editMakerSpaceAddress);
            
            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact]
        public void MakerSpaceIdDoesntExists()
        {
            //Arrange
            var editMakerSpaceAddress = _fixture.Fixture.Build<EditMakerSpaceAddress>().With(x => x.MakerSpaceId, Guid.NewGuid()).Create();
            
            //Act
            var result = _validator.TestValidate(editMakerSpaceAddress);
            
            //Assert
            result.ShouldHaveValidationErrorFor(x=>x.MakerSpaceId);
        }

    }
}
