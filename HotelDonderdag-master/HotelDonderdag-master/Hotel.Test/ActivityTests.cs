using Hotel.Domain.Exceptions;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Test
{
    public class ActivityTests
    {
        [Fact]
        public void Constructor_ValidData_CreatesObject()
        {
            // Arrange
            var organizer = new Organizer(); 
            string description = "Yoga Session";
            string location = "Park";
            int duration = 60;
            string activityName = "Morning Yoga";
            DateTime dateScheduled = DateTime.Now.AddDays(1);
            int availableSpots = 20;
            decimal adultPrice = 15.00m;
            decimal childPrice = 10.00m;
            decimal discount = 5.00m;

            // Act
            var activity = new Activity(organizer, description, location, duration, activityName, dateScheduled, availableSpots, adultPrice, childPrice, discount);

            // Assert
            Assert.NotNull(activity);
            Assert.Equal(description, activity.Description);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Description_SetInvalidValue_ThrowsActivityException(string invalidDescription)
        {
            // Arrange
            var organizer = new Organizer(); 

            // Act & Assert
            var exception = Assert.Throws<ActivityException>(() =>
                new Activity(organizer, invalidDescription, "Park", 60, "Morning Yoga", DateTime.Now.AddDays(1), 20, 15.00m, 10.00m, 5.00m));

            Assert.Equal("Description cannot be null or whitespace.", exception.Message);
        }

        [Fact]
        public void Duration_SetNegativeValue_ThrowsActivityException()
        {
            // Arrange & Act & Assert
            // Initialize with valid data except for duration
            var exception = Assert.Throws<ActivityException>(() =>
                new Activity(new Organizer(), "Yoga Session", "Park", -1, "Morning Yoga", DateTime.Now.AddDays(1), 20, 15.00m, 10.00m, 5.00m));

            Assert.Equal("Duration must be positive.", exception.Message);
        }

        private readonly Organizer validOrganizer = new Organizer();
        private readonly DateTime futureDate = DateTime.Now.AddDays(1);

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Location_SetInvalidValue_ThrowsActivityException(string invalidLocation)
        {
            var exception = Assert.Throws<ActivityException>(() =>
                new Activity(validOrganizer, "Yoga Session", invalidLocation, 60, "Morning Yoga", futureDate, 20, 15.00m, 10.00m, 5.00m));
            Assert.Equal("Location cannot be null or whitespace.", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ActivityName_SetInvalidValue_ThrowsActivityException(string invalidActivityName)
        {
            var exception = Assert.Throws<ActivityException>(() =>
                new Activity(validOrganizer, "Yoga Session", "Park", 60, invalidActivityName, futureDate, 20, 15.00m, 10.00m, 5.00m));
            Assert.Equal("Activity name cannot be null or whitespace.", exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Duration_SetInvalidValue_ThrowsActivityException(int invalidDuration)
        {
            var exception = Assert.Throws<ActivityException>(() =>
                new Activity(validOrganizer, "Yoga Session", "Park", invalidDuration, "Morning Yoga", futureDate, 20, 15.00m, 10.00m, 5.00m));
            Assert.Equal("Duration must be positive.", exception.Message);
        }

        [Theory]
        [InlineData(-1)]
        public void AvailableSpots_SetInvalidValue_ThrowsActivityException(int invalidAvailableSpots)
        {
            var exception = Assert.Throws<ActivityException>(() =>
                new Activity(validOrganizer, "Yoga Session", "Park", 60, "Morning Yoga", futureDate, invalidAvailableSpots, 15.00m, 10.00m, 5.00m));
            Assert.Equal("Available spots cannot be negative.", exception.Message);
        }

        [Theory]
        [InlineData(-1.0)]
        public void AdultPrice_SetInvalidValue_ThrowsActivityException(decimal invalidAdultPrice)
        {
            var exception = Assert.Throws<ActivityException>(() =>
                new Activity(validOrganizer, "Yoga Session", "Park", 60, "Morning Yoga", futureDate, 20, invalidAdultPrice, 10.00m, 5.00m));
            Assert.Equal("Adult price cannot be negative.", exception.Message);
        }

        [Theory]
        [InlineData(-1.0)]
        public void ChildPrice_SetInvalidValue_ThrowsActivityException(decimal invalidChildPrice)
        {
            var exception = Assert.Throws<ActivityException>(() =>
                new Activity(validOrganizer, "Yoga Session", "Park", 60, "Morning Yoga", futureDate, 20, 15.00m, invalidChildPrice, 5.00m));
            Assert.Equal("Child price cannot be negative.", exception.Message);
        }

        [Theory]
        [InlineData(-1.0)]
        public void Discount_SetInvalidValue_ThrowsActivityException(decimal invalidDiscount)
        {
            var exception = Assert.Throws<ActivityException>(() =>
                new Activity(validOrganizer, "Yoga Session", "Park", 60, "Morning Yoga", futureDate, 20, 15.00m, 10.00m, invalidDiscount));
            Assert.Equal("Discount cannot be negative.", exception.Message);
        }

    }
}
