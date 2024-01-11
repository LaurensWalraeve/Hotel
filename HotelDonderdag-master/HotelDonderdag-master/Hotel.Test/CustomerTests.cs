using Hotel.Domain.Exceptions;
using Hotel.Domain.Model;
using Xunit;

namespace Hotel.Test
{
    public class CustomerTests
    {
        [Fact]
        public void Constructor_WithIdNameContact_CreatesObject()
        {
            // Arrange
            int id = 1;
            string name = "John Doe";
            var contact = new ContactInfo("john@example.com", "1234567890", new Address("City", "Street", "12345", "101"));

            // Act
            var customer = new Customer(id, name, contact);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(id, customer.Id);
            Assert.Equal(name, customer.Name);
            Assert.Equal(contact, customer.Contact);
        }

        [Fact]
        public void Constructor_WithNameContact_CreatesObject()
        {
            // Arrange
            string name = "Jane Doe";
            var contact = new ContactInfo("jane@example.com", "0987654321", new Address("City", "Street", "54321", "102"));

            // Act
            var customer = new Customer(name, contact);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(name, customer.Name);
            Assert.Equal(contact, customer.Contact);
        }

        [Fact]
        public void Constructor_WithNameContactStatus_CreatesObject()
        {
            // Arrange
            string name = "Alice Doe";
            var contact = new ContactInfo("alice@example.com", "1112223333", new Address("City", "Street", "67890", "103"));
            bool status = true;

            // Act
            var customer = new Customer(name, contact, status);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(name, customer.Name);
            Assert.Equal(contact, customer.Contact);
            Assert.Equal(status, customer.Status);
        }

        [Fact]
        public void AddMember_ValidMember_AddsMember()
        {
            // Arrange
            var customer = new Customer();
            var member = new Member();

            // Act
            customer.AddMember(member);

            // Assert
            Assert.Contains(member, customer.GetMembers());
        }

        [Fact]
        public void AddMember_DuplicateMember_ThrowsCustomerException()
        {
            // Arrange
            var customer = new Customer();
            var member = new Member();
            customer.AddMember(member);

            // Act & Assert
            var exception = Assert.Throws<CustomerException>(() => customer.AddMember(member));
            Assert.Equal("AddMember", exception.Message);
        }

        [Fact]
        public void RemoveMember_ExistingMember_RemovesMember()
        {
            // Arrange
            var customer = new Customer();
            var member = new Member();
            customer.AddMember(member);

            // Act
            customer.RemoveMember(member);

            // Assert
            Assert.DoesNotContain(member, customer.GetMembers());
        }

        [Fact]
        public void RemoveMember_NonExistingMember_ThrowsCustomerException()
        {
            // Arrange
            var customer = new Customer();
            var member = new Member();

            // Act & Assert
            var exception = Assert.Throws<CustomerException>(() => customer.RemoveMember(member));
            Assert.Equal("RemoveMember", exception.Message);
        }
    }
}
