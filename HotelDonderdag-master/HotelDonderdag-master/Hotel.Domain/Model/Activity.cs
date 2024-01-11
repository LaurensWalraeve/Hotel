using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Activity
    {
        public Activity(Organizer organizer, string description, string location, int duration, string activityName, DateTime dateScheduled, int availableSpots, decimal adultPrice, decimal childPrice, decimal discount)
        {
            Organizer = organizer;
            Description = description;
            Location = location;
            Duration = duration;
            ActivityName = activityName;
            DateScheduled = dateScheduled;
            AvailableSpots = availableSpots;
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            Discount = discount;
            CustomerRegistrations = new HashSet<CustomerRegistration>();
        }

        public Activity(int activityID, Organizer organizer, string description, string location, int duration, string activityName, DateTime dateScheduled, int availableSpots, decimal adultPrice, decimal childPrice, decimal discount)
        {
            ActivityID = activityID;
            Organizer = organizer;
            Description = description;
            Location = location;
            Duration = duration;
            ActivityName = activityName;
            DateScheduled = dateScheduled;
            AvailableSpots = availableSpots;
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            Discount = discount;
            CustomerRegistrations = new HashSet<CustomerRegistration>();
        }

        public Activity(int activityID, Organizer organizer, string description, string location, int duration, string activityName, DateTime dateScheduled, int availableSpots, decimal adultPrice, decimal childPrice, decimal discount, bool status)
        {
            ActivityID = activityID;
            Organizer = organizer;
            Description = description;
            Location = location;
            Duration = duration;
            ActivityName = activityName;
            DateScheduled = dateScheduled;
            AvailableSpots = availableSpots;
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            Discount = discount;
            Status = status;
            CustomerRegistrations = new HashSet<CustomerRegistration>();
        }

        public Activity()
        {

        }

        public int ActivityID { get; set; }
        public Organizer Organizer { get; set; }
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ActivityException("Description cannot be null or whitespace.");
                _description = value;
            }
        }
        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ActivityException("Location cannot be null or whitespace.");
                _location = value;
            }
        }
        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value <= 0)
                    throw new ActivityException("Duration must be positive.");
                _duration = value;
            }
        }
        private string _activityName;
        public string ActivityName
        {
            get => _activityName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ActivityException("Activity name cannot be null or whitespace.");
                _activityName = value;
            }
        }
        public DateTime DateScheduled { get; set; }
        private int _availableSpots;
        public int AvailableSpots
        {
            get => _availableSpots;
            set
            {
                if (value < 0)
                    throw new ActivityException("Available spots cannot be negative.");
                _availableSpots = value;
            }
        }
        private decimal _adultPrice;
        public decimal AdultPrice
        {
            get => _adultPrice;
            set
            {
                if (value < 0)
                    throw new ActivityException("Adult price cannot be negative.");
                _adultPrice = value;
            }
        }
        private decimal _childPrice;
        public decimal ChildPrice
        {
            get => _childPrice;
            set
            {
                if (value < 0)
                    throw new ActivityException("Child price cannot be negative.");
                _childPrice = value;
            }
        }
        private decimal _discount;
        public decimal Discount
        {
            get => _discount;
            set
            {
                if (value < 0)
                    throw new ActivityException("Discount cannot be negative.");
                _discount = value;
            }
        }
        public bool Status { get; set; }
        public virtual ICollection<CustomerRegistration> CustomerRegistrations { get; set; }

    }
}
