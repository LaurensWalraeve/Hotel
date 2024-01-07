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
        public string Description { get; set; }
        public string Location { get; set; }
        public int Duration { get; set; }
        public string ActivityName { get; set; }
        public DateTime DateScheduled { get; set; }
        public int AvailableSpots { get; set; }
        public decimal AdultPrice { get; set; }
        public decimal ChildPrice { get; set; }
        public decimal Discount { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<CustomerRegistration> CustomerRegistrations { get; set; }
    }
}
