using Hotel.Domain.Exceptions;
using Hotel.Domain.Model;
using Xunit;

namespace Hotel.Test
{
    public class RegistrationMemberTests
    {
        [Fact]
        public void Constructor_ValidData_CreatesObject()
        {
            // Arrange
            var customer = new Customer("John Doe", new ContactInfo("john@example.com", "1234567890", new Address("City", "Street", "12345", "101")));
            var activity = new Activity();
            var member = new Member("Member Name", DateOnly.FromDateTime(DateTime.Now.AddYears(-20)));
            bool status = true;

            // Act
            var registrationMember = new RegistrationMember(customer, activity, member, status);

            // Assert
            Assert.NotNull(registrationMember);
            Assert.Equal(customer, registrationMember.Customer);
            Assert.Equal(activity, registrationMember.Activity);
            Assert.Equal(member, registrationMember.Member);
            Assert.Equal(status, registrationMember.Status);
        }

        [Fact]
        public void Constructor_NullCustomer_ThrowsReservationException()
        {
            // Arrange
            Customer customer = null;
            var activity = new Activity();
            var member = new Member("Member Name", DateOnly.FromDateTime(DateTime.Now.AddYears(-20)));

            // Act & Assert
            var exception = Assert.Throws<ReservationException>(() => new RegistrationMember(customer, activity, member));
            Assert.Equal("Customer cannot be null.", exception.Message);
        }

        [Fact]
        public void Constructor_NullActivity_ThrowsReservationException()
        {
            // Arrange
            var customer = new Customer("Jane Doe", new ContactInfo("jane@example.com", "0987654321", new Address("City", "Street", "54321", "102")));
            Activity activity = null;
            var member = new Member("Member Name", DateOnly.FromDateTime(DateTime.Now.AddYears(-20)));

            // Act & Assert
            var exception = Assert.Throws<ReservationException>(() => new RegistrationMember(customer, activity, member));
            Assert.Equal("Activity cannot be null.", exception.Message);
        }

        [Fact]
        public void Constructor_NullMember_ThrowsReservationException()
        {
            // Arrange
            var customer = new Customer("Alice Doe", new ContactInfo("alice@example.com", "1112223333", new Address("City", "Street", "67890", "103")));
            var activity = new Activity();
            Member member = null;

            // Act & Assert
            var exception = Assert.Throws<ReservationException>(() => new RegistrationMember(customer, activity, member));
            Assert.Equal("Member cannot be null.", exception.Message);
        }
    }
}
