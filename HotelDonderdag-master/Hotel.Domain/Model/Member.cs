using Hotel.Domain.Exceptions;

namespace Hotel.Domain.Model
{
    public class Member
    {
        public Member(string name, DateOnly birthday)
        {
            Name = name;
            Birthday = birthday;
        }

        public Member(string name, DateOnly birthday, bool status)
        {
            Name = name;
            Birthday = birthday;
            Status = status;

        }

        public Member(string name, DateOnly birthday, Customer customer) 
        {
            Name = name;
            Birthday = birthday;
            Customer = customer;
        }

        public Member() { }

        private string _name;
        public string Name { get { return _name; } set { if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("member"); _name = value; } }
        private DateOnly _birthday;
        public DateOnly Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                if (DateOnly.FromDateTime(DateTime.Now) <= value) throw new CustomerException("member");
                _birthday = value;
            }
        }

        public Customer Customer { get; set; } 

        public bool Status { get; set; }     

        public override bool Equals(object? obj)
        {
            return obj is Member member &&
                   _name == member._name &&
                   _birthday.Equals(member._birthday);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_name, _birthday);
        }
    }
}