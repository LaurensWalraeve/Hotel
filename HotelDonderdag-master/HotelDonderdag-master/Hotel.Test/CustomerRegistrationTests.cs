using Hotel.Domain.Exceptions;
using Hotel.Domain.Model;
using Xunit;

namespace Hotel.Test
{
    public class CustomerRegistrationTests
    {
        [Fact]
        public void Constructor_ValidData_CreatesObject()
        {
            // Arrange
            var customer = new Customer("John Doe", new ContactInfo("john@example.com", "1234567890", new Address("City", "Street", "12345", "101")));
            var activity = new Activity();
            decimal totalCost = 100.0m;
            bool status = true;

            // Act
            var customerRegistration = new CustomerRegistration(customer, activity, totalCost, status);

            // Assert
            Assert.NotNull(customerRegistration);
            Assert.Equal(customer, customerRegistration.Customer);
            Assert.Equal(activity, customerRegistration.Activity);
            Assert.Equal(totalCost, customerRegistration.TotalCost);
            Assert.Equal(status, customerRegistration.Status);
        }

        [Fact]
        public void Constructor_NullCustomer_ThrowsReservationException()
        {
            // Arrange
            Customer customer = null;
            var activity = new Activity();
            decimal totalCost = 100.0m;

            // Act & Assert
            var exception = Assert.Throws<ReservationException>(() => new CustomerRegistration(customer, activity, totalCost));
            Assert.Equal("Customer cannot be null.", exception.Message);
        }

        [Fact]
        public void Constructor_NullActivity_ThrowsReservationException()
        {
            // Arrange
            var customer = new Customer("Jane Doe", new ContactInfo("jane@example.com", "0987654321", new Address("City", "Street", "54321", "102")));
            Activity activity = null;
            decimal totalCost = 100.0m;

            // Act & Assert
            var exception = Assert.Throws<ReservationException>(() => new CustomerRegistration(customer, activity, totalCost));
            Assert.Equal("Activity cannot be null.", exception.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_NegativeTotalCost_ThrowsReservationException(decimal totalCost)
        {
            // Arrange
            var customer = new Customer("Alice Doe", new ContactInfo("alice@example.com", "1112223333", new Address("City", "Street", "67890", "103")));
            var activity = new Activity();

            // Act & Assert
            var exception = Assert.Throws<ReservationException>(() => new CustomerRegistration(customer, activity, totalCost));
            Assert.Equal("Total cost cannot be negative.", exception.Message);
        }
    }
}
