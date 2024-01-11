using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Organizer
    {
        // Constructor for creating a new organizer (without ID)
        public Organizer(string name, string email, string phone, Address address)
        {
            ValidateOrganizer(name, email, address);
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;

        }

        // Constructor for creating an organizer with an existing ID
        public Organizer(int organizerID, string name, string email, string phone, Address address, bool status)
        {
            ValidateOrganizer(name, email, address);
            OrganizerID = organizerID;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            Status = status;

        }

        public Organizer(int organizerID, string name, string email, string phone, Address address)
        {
            ValidateOrganizer(name, email, address);
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
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new OrganizerException("Name cannot be null or whitespace.");
                _name = value;
            }
        }
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (!IsValidEmail(value))
                    throw new OrganizerException("Invalid email format.");
                _email = value;
            }
        }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }

        private void ValidateOrganizer(string name, string email, Address address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new OrganizerException("Name cannot be null or whitespace.");

            if (!IsValidEmail(email))
                throw new OrganizerException("Invalid email format.");

            if (address == null)
                throw new OrganizerException("Address cannot be null.");
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}

