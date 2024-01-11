using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Persistence.Repositories;
using Hotel.Presentation.CustomerWPF.Model; // Assuming this is where your models are defined
using Hotel.Presentation.OrganizerWPF.Model;
using Hotel.Util;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows;

namespace Hotel.Presentation.ReservationWPF
{
    /// <summary>
    /// Interaction logic for OverviewWindow.xaml
    /// </summary>
    public partial class OverviewWindow : Window
    {
        private CustomerUI _selectedCustomer;
        private List<MemberUI> _selectedMembers;
        private ActivityUI _selectedActivity;

        private ReservationManager reservationManager;

        public OverviewWindow(CustomerUI selectedCustomer, List<MemberUI> selectedMembers, ActivityUI selectedActivity)
        {
            InitializeComponent();
            _selectedCustomer = selectedCustomer;
            _selectedMembers = selectedMembers;
            _selectedActivity = selectedActivity;
            reservationManager = new ReservationManager(RepositoryFactory.ReservationRepository);
            DisplayDetails();
        }

        private void DisplayDetails()
        {
            // Displaying customer details
            CustomerNameTextBox.Text = _selectedCustomer.Name;

            // Displaying member details
            MembersListBox.ItemsSource = _selectedMembers.Select(m => $"{m.Name} - {m.Birthday.ToShortDateString()}");

            // Displaying activity details
            ActivityNameTextBox.Text = _selectedActivity.ActivityName;
            // Optionally add more activity details here
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Create CustomerRegistration object
                var customerRegistration = new CustomerRegistration
                {
                    Customer = new Customer { Id = _selectedCustomer.Id.Value },
                    Activity = new Activity { ActivityID = _selectedActivity.ActivityID },
                    TotalCost = CalculateTotalCost(), // Implement this method as needed
                                                      // Status = true; // If needed
                };

                // Add Customer Registration
                reservationManager.AddRegistrationCustomer(customerRegistration);

                // Add each Member Registration
                foreach (var memberUI in _selectedMembers)
                {


                    var registrationMember = new RegistrationMember
                    {
                        Customer = new Customer { Id = _selectedCustomer.Id.Value },
                        Activity = new Activity { ActivityID = _selectedActivity.ActivityID },
                        Member = new Member { Name = memberUI.Name, Birthday = memberUI.Birthday },
                        // Status = true; // If needed
                    };

                    reservationManager.AddRegistrationMember(registrationMember);
                }

                MessageBox.Show("Reservering succesvol toegevoegd.", "Bevestiging", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private decimal CalculateTotalCost()
        {
            decimal totalCost = 0;
            foreach (var memberUI in _selectedMembers)
            {
                // Convert DateOnly to DateTime
                DateTime birthdayDateTime = memberUI.Birthday.ToDateTime(new TimeOnly());

                if (birthdayDateTime.AddYears(18) <= DateTime.Today)
                {
                    totalCost += _selectedActivity.AdultPrice;
                }
                else
                {
                    totalCost += _selectedActivity.ChildPrice;
                }
            }

            return totalCost;
        }


    }
}
