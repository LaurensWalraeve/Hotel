using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.CustomerWPF.Model
{
    public class MemberUI : INotifyPropertyChanged
    {
        public MemberUI(string name, DateTime birthday, int customerID)
        {
            Name = name;
            Birthday = birthday;
            CustomerID = customerID;
        }

        public MemberUI(int? id, string name, DateTime birthday, int customerID)
        {
            Id = id;
            Name = name;
            Birthday = birthday;
            CustomerID = customerID;
        }

        public int? Id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private DateTime _birthday;
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(Birthday));
            }
        }

        public int CustomerID { get; set; } // Assuming CustomerID is just an integer and doesn't need property change notification

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
