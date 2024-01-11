using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.OrganizerWPF.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ManageActivity.xaml
    /// </summary>
    public partial class ManageActivity : Window
    {
        public ActivityUI ActivityUI { get; set; }
        private ActivityManager activityManager;
        private readonly int? organizerID;
        public ManageActivity(ActivityUI activityUI, int? organizerID)
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            this.organizerID = organizerID;
            this.ActivityUI = activityUI;

            if (activityUI != null )
            {
                ActivityIdTextBox.Text = activityUI.ActivityID.ToString();
                ActivityNameTextBox.Text = activityUI.ActivityName;
                DescriptionTextBox.Text = activityUI.Description;
                LocationTextBox.Text = activityUI.Location;
                DurationTextBox.Text = activityUI.Duration.ToString();
                DateScheduledPicker.SelectedDate = activityUI.DateScheduled;
                AvailableSpotsTextBox.Text = activityUI.AvailableSpots.ToString();
                AdultPriceTextBox.Text = activityUI.AdultPrice.ToString("F2");
                ChildPriceTextBox.Text = activityUI.ChildPrice.ToString("F2");
                DiscountTextBox.Text = activityUI.Discount.ToString("F2");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!organizerID.HasValue)
            {
                MessageBox.Show("Organizer ID is not available.", "Error");
                return; 
            }

            

            if (ActivityUI == null)
            {

                Organizer organizer = new Organizer(organizerID.Value);
                Domain.Model.Activity activity = new Domain.Model.Activity(organizer, DescriptionTextBox.Text, LocationTextBox.Text, Convert.ToInt32(DurationTextBox.Text), ActivityNameTextBox.Text, Convert.ToDateTime(DateScheduledPicker.SelectedDate), Convert.ToInt32(AvailableSpotsTextBox.Text), Convert.ToDecimal(AdultPriceTextBox.Text), Convert.ToDecimal(ChildPriceTextBox.Text), Convert.ToDecimal(DiscountTextBox.Text));

                activityManager.UpdateActivity(activity);



                activityManager.AddActivity(activity);
            }
            else
            {
                
                Organizer organizer = new Organizer(organizerID.Value);

                Domain.Model.Activity activity = new Domain.Model.Activity(Convert.ToInt32(ActivityIdTextBox.Text), organizer, DescriptionTextBox.Text, LocationTextBox.Text, Convert.ToInt32(DurationTextBox.Text), ActivityNameTextBox.Text, Convert.ToDateTime(DateScheduledPicker.SelectedDate), Convert.ToInt32(AvailableSpotsTextBox.Text), Convert.ToDecimal(AdultPriceTextBox.Text), Convert.ToDecimal(ChildPriceTextBox.Text), Convert.ToDecimal(DiscountTextBox.Text));

                activityManager.UpdateActivity(activity);
            }
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
