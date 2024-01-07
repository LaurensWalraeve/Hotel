using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class RegistrationMember
    {
        // Constructor for creating a new RegistrationMember
        public RegistrationMember(Customer customer, Activity activity, Member member, bool status)
        {
            Customer = customer;
            Activity = activity;
            Member = member;
            Status = status;
        }

        public RegistrationMember(Customer customer, Activity activity, Member member)
        {
            Customer = customer;
            Activity = activity;
            Member = member;
        }

        public RegistrationMember() { }



        public Customer Customer { get; set; }
        public Activity Activity { get; set; }
        public Member Member { get; set; }
        public bool Status { get; set; }

        // Navigation property for the customer registration
        public virtual CustomerRegistration CustomerRegistration { get; set; }
    }
}
