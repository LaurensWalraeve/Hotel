using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class CustomerRegistration
    {
        // Constructor for creating a new CustomerRegistration
        public CustomerRegistration(Customer customer, Activity activity, decimal totalCost, bool status)
        {
            Customer = customer;
            Activity = activity;
            TotalCost = totalCost;
            RegistrationMembers = new HashSet<RegistrationMember>();  // Initialize the collection
            Status = status;
        }
        public CustomerRegistration(Customer customer, Activity activity, decimal totalCost)
        {
            Customer = customer;
            Activity = activity;
            TotalCost = totalCost;
            RegistrationMembers = new HashSet<RegistrationMember>();  // Initialize the collection
        }

        public CustomerRegistration() { }

        public Customer Customer { get; set; }
        public Activity Activity { get; set; }
        public decimal TotalCost { get; set; }
        public bool Status { get; set; }

        // Navigation property for registration members
        public virtual ICollection<RegistrationMember> RegistrationMembers { get; set; }
    }
}
