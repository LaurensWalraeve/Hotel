using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.OrganizerWPF.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace Hotel.Presentation.OrganizerWPF
{
    /// <summary>
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        private ObservableCollection<ActivityUI> activitiesUIs = new ObservableCollection<ActivityUI>();
        private ActivityManager activityManager;
        private OrganizerUI organizerUI;

        public ActivityWindow(OrganizerUI organizerUI)
        {
            InitializeComponent();
            this.organizerUI = organizerUI;
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            LoadActivities();
        }

        private void LoadActivities()
        {
            activitiesUIs.Clear();
            var activities = activityManager.GetActivities(organizerUI.OrganizerID.ToString());
            foreach (var activity in activities)
            {
                activitiesUIs.Add(new ActivityUI(activity.ActivityID, activity.Description, activity.Location, activity.Duration, activity.ActivityName, activity.DateScheduled, activity.AvailableSpots, activity.AdultPrice, activity.ChildPrice, activity.Discount));
            }
            ActivityDataGrid.ItemsSource = activitiesUIs;
            OrganizerInfo.Text = organizerUI.Name;
        }

        private void MenuItemAddActivity_Click(object sender, RoutedEventArgs e)
        {
            ManageActivity w = new ManageActivity(null, organizerUI.OrganizerID);
            w.Closed += (s, args) => LoadActivities(); // Refresh members list when ManageMember window closes
            w.ShowDialog();
        }

        private void MenuItemDeleteActivity_Click(object sender, RoutedEventArgs e)
        {
            if (ActivityDataGrid.SelectedItem == null) MessageBox.Show("not selected", "delete");
            else
            {
                ActivityUI activityUI = (ActivityUI)ActivityDataGrid.SelectedItem;

                //address
                // Splitting the address into parts
                string[] parts = organizerUI.Address.Split(new[] { " - " }, StringSplitOptions.None);

                // Extracting city and zip code
                string cityAndZip = parts[0];
                string city = cityAndZip.Split('[')[0].Trim();
                string zip = cityAndZip.Split('[')[1].Trim(']');

                // Extracting street and house number
                string street = parts[1];
                string houseNumber = parts[2];


                Address address = new Address(city, street, zip, houseNumber);


                Organizer organizer = new Organizer(organizerUI.OrganizerID, organizerUI.Name, organizerUI.Email, organizerUI.Phone, address);


                Domain.Model.Activity activity = new Domain.Model.Activity(activityUI.ActivityID, organizer, activityUI.Description, activityUI.Location, activityUI.Duration, activityUI.ActivityName, activityUI.DateScheduled, activityUI.AvailableSpots, activityUI.AdultPrice, activityUI.ChildPrice, activityUI.Discount);

                activityManager.DeleteActivity(activity);
                LoadActivities();
            }
        }

        private void MenuItemUpdateActivity_Click(object sender, RoutedEventArgs e)
        {
            if (ActivityDataGrid.SelectedItem == null) MessageBox.Show("not selected", "update");
            else
            {
                ActivityUI activityUI = (ActivityUI)ActivityDataGrid.SelectedItem;

                ManageActivity w = new ManageActivity(activityUI, organizerUI.OrganizerID);
                w.Closed += (s, args) => LoadActivities(); // Refresh members list when ManageMember window closes
                w.ShowDialog();
            }
        }
    }
}
