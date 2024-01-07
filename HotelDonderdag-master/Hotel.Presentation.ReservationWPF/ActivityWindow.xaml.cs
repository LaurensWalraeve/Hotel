using Hotel.Domain.Managers;
using Hotel.Presentation.CustomerWPF.Model;
using Hotel.Presentation.OrganizerWPF.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotel.Presentation.ReservationWPF
{
    /// <summary>
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        private ObservableCollection<ActivityUI> activitiesUIs = new ObservableCollection<ActivityUI>();
        private ActivityManager activityManager;
        private CustomerUI _customerUI;
        private List<MemberUI> _selectedMembers;
        public ActivityWindow(CustomerUI customerUI, List<MemberUI> selectedMembers)
        {
            InitializeComponent();
            _customerUI = customerUI;
            _selectedMembers = selectedMembers;

            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            LoadActivities();
        }

        private void LoadActivities()
        {
            activitiesUIs.Clear();
            var activities = activityManager.GetActivities(null);
            foreach (var activity in activities)
            {
                activitiesUIs.Add(new ActivityUI(activity.ActivityID, activity.Description, activity.Location, activity.Duration, activity.ActivityName, activity.DateScheduled, activity.AvailableSpots, activity.AdultPrice, activity.ChildPrice, activity.Discount));
            }
            ActivityDataGrid.ItemsSource = activitiesUIs;
            SearchTextBox.Text = _customerUI.Name;
        }

        private void ChooseActivityButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the selected activity from the DataGrid
            var selectedActivity = ActivityDataGrid.SelectedItem as ActivityUI;
            if (selectedActivity == null)
            {
                MessageBox.Show("Selecteer eerst een activiteit.", "Activiteit Niet Geselecteerd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Open the OverviewWindow with the selected customer, members, and activity
            var overviewWindow = new OverviewWindow(_customerUI, _selectedMembers, selectedActivity);
            var result = overviewWindow.ShowDialog(); // ShowDialog if you want to open it as a modal dialog

            // Handle the result if necessary
            if (result.HasValue && result.Value)
            {
                // Logic for what to do after the overview window is closed with confirmation
            }
        }

    }
}
