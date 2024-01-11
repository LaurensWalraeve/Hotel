using Hotel.Domain.Exceptions;
using Hotel.Domain.Model;
using System;
using Xunit;

namespace Hotel.Test
{
    public class MemberTests
    {
        [Fact]
        public void Constructor_ValidData_CreatesObject()
        {
            // Arrange
            string name = "John Doe";
            DateOnly birthday = DateOnly.FromDateTime(DateTime.Now.AddYears(-20));

            // Act
            var member = new Member(name, birthday);

            // Assert
            Assert.NotNull(member);
            Assert.Equal(name, member.Name);
            Assert.Equal(birthday, member.Birthday);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Name_InvalidData_ThrowsCustomerException(string invalidName)
        {
            // Arrange
            DateOnly birthday = DateOnly.FromDateTime(DateTime.Now.AddYears(-20));

            // Act & Assert
            var exception = Assert.Throws<CustomerException>(() => new Member(invalidName, birthday));
            Assert.Equal("member", exception.Message);
        }

        [Fact]
        public void Birthday_FutureDate_ThrowsCustomerException()
        {
            // Arrange
            string name = "Jane Doe";
            DateOnly futureBirthday = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

            // Act & Assert
            var exception = Assert.Throws<CustomerException>(() => new Member(name, futureBirthday));
            Assert.Equal("member", exception.Message);
        }

        [Fact]
        public void Equals_SameData_ReturnsTrue()
        {
            // Arrange
            string name = "Alice Doe";
            DateOnly birthday = DateOnly.FromDateTime(DateTime.Now.AddYears(-20));
            var member1 = new Member(name, birthday);
            var member2 = new Member(name, birthday);

            // Act
            bool areEqual = member1.Equals(member2);

            // Assert
            Assert.True(areEqual);
        }

        [Fact]
        public void Equals_DifferentData_ReturnsFalse()
        {
            // Arrange
            string name1 = "Alice Doe";
            string name2 = "Bob Smith";
            DateOnly birthday = DateOnly.FromDateTime(DateTime.Now.AddYears(-20));
            var member1 = new Member(name1, birthday);
            var member2 = new Member(name2, birthday);

            // Act
            bool areNotEqual = !member1.Equals(member2);

            // Assert
            Assert.True(areNotEqual);
        }
    }
}
