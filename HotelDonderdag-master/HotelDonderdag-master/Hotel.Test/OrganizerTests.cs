using Hotel.Domain.Exceptions;
using Hotel.Domain.Model;
using System.Text.RegularExpressions;
using Xunit;

namespace Hotel.Test
{
    public class OrganizerTests
    {
        [Fact]
        public void Constructor_ValidData_CreatesObject()
        {
            // Arrange
            string name = "John Doe";
            string email = "john@example.com";
            string phone = "1234567890";
            var address = new Address("City", "Street", "12345", "101");

            // Act
            var organizer = new Organizer(name, email, phone, address);

            // Assert
            Assert.NotNull(organizer);
            Assert.Equal(name, organizer.Name);
            Assert.Equal(email, organizer.Email);
            Assert.Equal(phone, organizer.Phone);
            Assert.Equal(address, organizer.Address);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_InvalidName_ThrowsOrganizerException(string invalidName)
        {
            // Arrange
            string email = "jane@example.com";
            string phone = "0987654321";
            var address = new Address("City", "Street", "54321", "102");

            // Act & Assert
            var exception = Assert.Throws<OrganizerException>(() => new Organizer(invalidName, email, phone, address));
            Assert.Equal("Name cannot be null or whitespace.", exception.Message);
        }

        [Theory]
        [InlineData("invalidemail")]
        [InlineData("invalid@")]
        [InlineData("invalid@domain")]
        public void Constructor_InvalidEmail_ThrowsOrganizerException(string invalidEmail)
        {
            // Arrange
            string name = "Alice Doe";
            string phone = "1112223333";
            var address = new Address("City", "Street", "67890", "103");

            // Act & Assert
            var exception = Assert.Throws<OrganizerException>(() => new Organizer(name, invalidEmail, phone, address));
            Assert.Equal("Invalid email format.", exception.Message);
        }

        [Fact]
        public void Constructor_NullAddress_ThrowsOrganizerException()
        {
            // Arrange
            string name = "Bob Smith";
            string email = "bob@example.com";
            string phone = "1231231234";

            // Act & Assert
            var exception = Assert.Throws<OrganizerException>(() => new Organizer(name, email, phone, null));
            Assert.Equal("Address cannot be null.", exception.Message);
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
