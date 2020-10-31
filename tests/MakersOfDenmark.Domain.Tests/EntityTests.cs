
using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Domain.Models;
using Xunit;

namespace MakersOfDenmark.Domain.Tests
{
    public class EntityTests
    {   
        private readonly Fixture _fixture;
        public EntityTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void EntityOfGuid_Equals_SameIdsDifferentObject_ReturnsTrue()
        {
            //Arrange
            var id = _fixture.Create<Guid>();

            var testObj1 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();
            var testObj2 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();

            //Act
            var subject = testObj1.Equals(testObj2);

            //Assert
            subject.Should().BeTrue();
        }

        [Fact]
        public void EntityOfGuid_Equals_DifferentIdsDifferentObject_ReturnsFalse()
        {
            //Arrange
            var id = _fixture.Create<Guid>();
            var notEqualId = _fixture.Create<Guid>();

            var testObj1 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();
            var testObj2 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, notEqualId).Create();

            //Act
            var subject = testObj1.Equals(testObj2);

            //Assert
            subject.Should().BeFalse();
        }

        [Fact]
        public void EntityOfGuid_Equals_null_ReturnsFalse()
        {
            //Arrange
            var id = _fixture.Create<Guid>();

            var testObj1 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();

            //Act
            var subject = testObj1.Equals(null);

            //Assert
            subject.Should().BeFalse();
        }

        [Fact]
        public void EntityOfGuid_EqualOperatorEquals_SameIdsDifferentObject_ReturnsTrue()
        {
            //Arrange
            var id = _fixture.Create<Guid>();

            var testObj1 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();
            var testObj2 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();

            //Act
            var subject = testObj1 == testObj2;

            //Assert
            subject.Should().BeTrue();
        }

        [Fact]
        public void EntityOfGuid_NotEqualOperatorEquals_DifferentIdsDifferentObject_ReturnsTrue()
        {
            //Arrange
            var id = _fixture.Create<Guid>();
            var idNotEqual = _fixture.Create<Guid>();

            var testObj1 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();
            var testObj2 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, idNotEqual).Create();

            //Act
            var subject = testObj1 != testObj2;

            //Assert
            subject.Should().BeTrue();
        }

        [Fact]
        public void EntityOfGuid_EqualOperator_null_ReturnsFalse()
        {
            //Arrange
            var id = _fixture.Create<Guid>();

            var testObj1 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();
            var testObj2 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();

            //Act
            var subject = testObj1 == null;

            //Assert
            subject.Should().BeFalse();
        }

        [Fact]
        public void EntityOfGuid_NotEqualOperatorEquals_null_ReturnsTrue()
        {
            //Arrange
            var id = _fixture.Create<Guid>();
            var testObj1 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();

            //Act
            var subject = testObj1 != null;

            //Assert
            subject.Should().BeTrue();
        }

        [Fact]
        public void EntityOfGuid_GetHashCode_DifferentIds_ReturnsFalse()
        {
            //Arrange
            var id = _fixture.Create<Guid>();
            var idNotEqual = _fixture.Create<Guid>();

            var testObj1 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();
            var testObj2 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, idNotEqual).Create();
            var expected = testObj2.GetHashCode();

            //Act
            var subject = testObj1.GetHashCode();

            //Assert
            subject.Should().NotBe(expected);
        }

        [Fact]
        public void EntityOfGuid_GetHashCode_SameIds_ReturnsTrue()
        {
            //Arrange
            var id = _fixture.Create<Guid>();

            var testObj1 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();
            var testObj2 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();
            var expected = testObj2.GetHashCode();

            //Act
            var subject = testObj1.GetHashCode();

            //Assert
            subject.Should().Be(expected);
        }

        [Fact]
        public void EntityOfInt_NotEqualOperatorEquals_nullReference_ReturnsTrue()
        {
            //Arrange
            TestEntityWithGuid testObj1 = null;

            //Act
            var subject = testObj1 != null;

            //Assert
            subject.Should().BeFalse();
        }


        [Fact]
        public void EntityOfT_HashSet_NoDuplicates() 
        {
            //Arrange
            var id = _fixture.Create<Guid>();

            var testObj1 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();
            var testObj2 = _fixture.Build<TestEntityWithGuid>().With(x => x.Id, id).Create();

            //Act
            var subject = new HashSet<TestEntityWithGuid>() {
                testObj1,
                testObj2
            };

            //Assert
            subject.Should().HaveCount(1);
        }
    }

    internal class TestEntityWithGuid : Entity<Guid>
    {
    }
}