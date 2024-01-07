using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Organizer
    {
        // Constructor for creating a new organizer (without ID)
        public Organizer(string name, string email, string phone, Address address)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;

        }

        // Constructor for creating an organizer with an existing ID
        public Organizer(int organizerID, string name, string email, string phone, Address address, bool status)
        {
            OrganizerID = organizerID;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            Status = status;

        }

        public Organizer(int organizerID, string name, string email, string phone, Address address)
        {
            OrganizerID = organizerID;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;

        }

        public Organizer(int organizerID)
        {
            OrganizerID = organizerID;
        }

        public Organizer() { }

        public int OrganizerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public bool Status { get; set; }

        // Navigation property for activities
        public virtual ICollection<Activity> Activities { get; set; }
    }
}

