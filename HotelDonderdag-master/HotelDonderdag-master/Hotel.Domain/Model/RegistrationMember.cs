using Hotel.Domain.Exceptions;
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
            ValidateParameters(customer, activity, member);
            Customer = customer;
            Activity = activity;
            Member = member;
            Status = status;
        }

        public RegistrationMember(Customer customer, Activity activity, Member member)
        {
            ValidateParameters(customer, activity, member);
            Customer = customer;
            Activity = activity;
            Member = member;
        }

        public RegistrationMember() { }



        public Customer Customer { get; set; }
        public Activity Activity { get; set; }
        public Member Member { get; set; }
        public bool Status { get; set; }

        public virtual CustomerRegistration CustomerRegistration { get; set; }

        private void ValidateParameters(Customer customer, Activity activity, Member member)
        {
            if (customer == null)
                throw new ReservationException("Customer cannot be null.");

            if (activity == null)
                throw new ReservationException("Activity cannot be null.");

            if (member == null)
                throw new ReservationException("Member cannot be null.");
        }
    }
}