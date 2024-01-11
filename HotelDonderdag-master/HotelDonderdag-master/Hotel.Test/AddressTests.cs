using Xunit;
using Hotel.Domain.Model;
using Hotel.Domain.Exceptions;

namespace Hotel.Test
{
    public class AddressTests
    {
        [Fact]
        public void Constructor_ValidData_CreatesObject()
        {
            // Arrange
            string city = "Springfield";
            string street = "Main St";
            string postalCode = "12345";
            string houseNumber = "101A";

            // Act
            var address = new Address(city, street, postalCode, houseNumber);

            // Assert
            Assert.NotNull(address);
            Assert.Equal(city, address.City);
            Assert.Equal(street, address.Street);
            Assert.Equal(postalCode, address.PostalCode);
            Assert.Equal(houseNumber, address.HouseNumber);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void City_InvalidData_ThrowsCustomerException(string invalidCity)
        {
            // Act & Assert
            var exception = Assert.Throws<CustomerException>(() => new Address(invalidCity, "Main St", "12345", "101A"));
            Assert.Equal("Mun is empty", exception.Message);
        }

        [Theory]
        [InlineData("Springfield|12345|Main St|101A")]
        public void ConstructorFromString_ValidData_CreatesObject(string addressLine)
        {
            // Act
            var address = new Address(addressLine);

            // Assert
            Assert.NotNull(address);
            Assert.Equal("Springfield", address.City);
            Assert.Equal("12345", address.PostalCode);
            Assert.Equal("Main St", address.Street);
            Assert.Equal("101A", address.HouseNumber);
        }

        [Theory]
        [InlineData("Springfield||Main St|101A")] // Missing postal code
        [InlineData("Springfield|12345||101A")]   // Missing street
        public void ConstructorFromString_InvalidData_ThrowsCustomerException(string invalidAddressLine)
        {
            // Act & Assert
            var exception = Assert.Throws<CustomerException>(() => new Address(invalidAddressLine));
            Assert.Contains("is empty", exception.Message); 
        }
    }
}

   
