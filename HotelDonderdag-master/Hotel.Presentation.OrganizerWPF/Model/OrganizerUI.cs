using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.OrganizerWPF.Model
{
    public class OrganizerUI : INotifyPropertyChanged
    {
        private int _organizerID;
        private string _name;
        private string _email;
        private string _phone;
        private string _address;

        public OrganizerUI(int organizerID, string name, string email, string phone, string address)
        {
            OrganizerID = organizerID;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public OrganizerUI(string name, string email, string phone, string address)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public int OrganizerID
        {
            get => _organizerID;
            set
            {
                _organizerID = value;
                OnPropertyChanged(nameof(OrganizerID));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
