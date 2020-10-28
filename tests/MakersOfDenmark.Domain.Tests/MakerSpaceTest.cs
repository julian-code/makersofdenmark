using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Domain.Models.User;
using System;
using Xunit;

namespace MakersOfDenmark.Domain.Tests
{
    public class MakerSpaceTest
    {
        private readonly Fixture _fixture;

        public MakerSpaceTest()
        {
            _fixture = new Fixture();
        }
        [Fact]
        public void FollowMakerSpace_defaultUser()
        {
            // Arrange
            var makerspace = _fixture.Build<MakerSpace>().Without(x => x.Tools).Without(x => x.Followers).Create();
            var defaultUser = _fixture.Build<MODUser>().Without(x => x.Follows).Without(x => x.Roles).Create();

            // Act
            makerspace.Followers.Should().HaveCount(0);

            makerspace.FollowMakerSpace(defaultUser);

            // Assert
            makerspace.Followers.Should().HaveCount(1);
            // makerspace.Followers.Should().Contain(x=> x.User,defaultUser);
        }
        [Fact]
        public void UnfollowMakerSpace_DefaultUser()
        {
            // Arrange
            var makerspace = _fixture.Build<MakerSpace>().Without(x => x.Tools).Without(x => x.Followers).Create();
            var defaultUser = _fixture.Build<MODUser>().Without(x => x.Follows).Without(x => x.Roles).Create();
            var user2 = _fixture.Build<MODUser>().Without(x => x.Follows).Without(x => x.Roles).Create();
            var user3 = _fixture.Build<MODUser>().Without(x => x.Follows).Without(x => x.Roles).Create();
            makerspace.FollowMakerSpace(defaultUser);
            makerspace.FollowMakerSpace(user2);
            makerspace.FollowMakerSpace(user3);

            // Act
            makerspace.Followers.Should().HaveCount(3);
            makerspace.UnfollowMakerSpace(defaultUser);

            // Assert
            makerspace.Followers.Should().HaveCount(2);
        }
    }
}
