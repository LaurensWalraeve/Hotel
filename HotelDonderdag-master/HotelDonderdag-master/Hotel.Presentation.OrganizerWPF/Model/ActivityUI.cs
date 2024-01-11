using System;
using System.ComponentModel;

namespace Hotel.Presentation.OrganizerWPF.Model
{
    public class ActivityUI : INotifyPropertyChanged
    {

        private int _activityID;
        private string _description;
        private string _location;
        private int _duration;
        private string _activityName;
        private DateTime _dateScheduled;
        private int _availableSpots;
        private decimal _adultPrice;
        private decimal _childPrice;
        private decimal _discount;

        public ActivityUI(int activityID, string description, string location, int duration, string activityName, DateTime dateScheduled, int availableSpots, decimal adultPrice, decimal childPrice, decimal discount)
        {
            ActivityID = activityID;
            Description = description;
            Location = location;
            Duration = duration;
            ActivityName = activityName;
            DateScheduled = dateScheduled;
            AvailableSpots = availableSpots;
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            Discount = discount;
        }

        public int ActivityID
        {
            get => _activityID;
            set { _activityID = value; OnPropertyChanged(nameof(ActivityID)); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        public string Location
        {
            get => _location;
            set { _location = value; OnPropertyChanged(nameof(Location)); }
        }

        public int Duration
        {
            get => _duration;
            set { _duration = value; OnPropertyChanged(nameof(Duration)); }
        }

        public string ActivityName
        {
            get => _activityName;
            set { _activityName = value; OnPropertyChanged(nameof(ActivityName)); }
        }

        public DateTime DateScheduled
        {
            get => _dateScheduled;
            set { _dateScheduled = value; OnPropertyChanged(nameof(DateScheduled)); }
        }

        public int AvailableSpots
        {
            get => _availableSpots;
            set { _availableSpots = value; OnPropertyChanged(nameof(AvailableSpots)); }
        }

        public decimal AdultPrice
        {
            get => _adultPrice;
            set { _adultPrice = value; OnPropertyChanged(nameof(AdultPrice)); }
        }

        public decimal ChildPrice
        {
            get => _childPrice;
            set { _childPrice = value; OnPropertyChanged(nameof(ChildPrice)); }
        }

        public decimal Discount
        {
            get => _discount;
            set { _discount = value; OnPropertyChanged(nameof(Discount)); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
