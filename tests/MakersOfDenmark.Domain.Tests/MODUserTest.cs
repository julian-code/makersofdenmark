using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Domain.Enums;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MakersOfDenmark.Domain.Tests
{
    public class MODUserTest
    {
        private readonly Fixture _fixture;
        public MODUserTest()
        {
            _fixture = new Fixture();
        }
        [Fact]
        public void CreateUser()
        {
            // Arrange
            var expected = _fixture.Build<MODUser>().Without(x => x.Follows).Without(x => x.Roles).Without(x=> x.CreatedAt).Create();

            // Act
            var actual = MODUser.CreateUser(expected.FirstName, expected.LastName, expected.Email);

            // Assert
            actual.Should().BeEquivalentTo(expected, options => options.Excluding(x=>x.Id).Excluding(x=>x.CreatedAt));
        }
        [Theory]
        [InlineData(MakerSpaceRoles.Admin)]
        [InlineData(MakerSpaceRoles.Staff)]
        public void AddMakerSpaceRole_defaultUser_BecomesRole(MakerSpaceRoles role)
        {
            // Arrange
            var makerspace = _fixture.Build<MakerSpace>().Without(x => x.Tools).Create();
            var defaultUser = _fixture.Build<MODUser>().Without(x => x.Roles).Without(x => x.Follows).Create();

            // Act
            defaultUser.AddMakerSpaceRole(makerspace, role);

            // Assert
            defaultUser.Roles.Should().HaveCount(1).And.ContainSingle(x => x.Role == role && x.MakerSpace == makerspace);
        }
        [Fact]
        public void RemoveMakerSpaceRole_defaultUser()
        {
            // Arrange
            var makerspace = _fixture.Build<MakerSpace>().Without(x => x.Tools).Create();
            var defaultUser = _fixture.Build<MODUser>().Without(x => x.Roles).Without(x => x.Follows).Create();
            defaultUser.AddMakerSpaceRole(makerspace, MakerSpaceRoles.Admin);
            var role = defaultUser.Roles.FirstOrDefault(x => x.Role == MakerSpaceRoles.Admin);

            // Act
            defaultUser.Roles.Should().HaveCount(1);
            defaultUser.RemoveMakerSpaceRole(makerspace, MakerSpaceRoles.Admin);

            // Assert
            defaultUser.Roles.Should().HaveCount(0);
            defaultUser.Roles.Should().NotContain(role);
        }

    }
}
