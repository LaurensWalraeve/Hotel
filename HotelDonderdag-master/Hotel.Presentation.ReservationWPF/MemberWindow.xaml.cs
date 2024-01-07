using Hotel.Domain.Managers;
using Hotel.Presentation.CustomerWPF.Model;
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
    /// Interaction logic for MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {
        private ObservableCollection<MemberUI> memberUIs = new ObservableCollection<MemberUI>();
        private MemberManager memberManager;
        private CustomerUI customerUI; // Make customerUI a class-level variable
        public MemberWindow(CustomerUI customerUI)
        {
            InitializeComponent();
            this.customerUI = customerUI; // Assign the passed customerUI to the class-level variable
            memberManager = new MemberManager(RepositoryFactory.MemberRepository);
            LoadMembers(); // Load members initially
        }

        private void LoadMembers()
        {
            memberUIs.Clear();
            var members = memberManager.GetMembers(customerUI.Id.ToString());
            foreach (var member in members)
            {
                memberUIs.Add(new MemberUI(member.Name, member.Birthday, member.Customer.Id));
            }
            MembersDataGrid.ItemsSource = memberUIs; // Set the ItemsSource
            SearchTextBox.Text = customerUI.Name; // Update customer info
        }

        private void ChooseMemberButton_Click(object sender, RoutedEventArgs e)
        {
            List<MemberUI> selectedMembers = new List<MemberUI>();

            // Iterate over the selected items in the DataGrid
            foreach (var item in MembersDataGrid.SelectedItems)
            {
                if (item is MemberUI selectedMember)
                {
                    selectedMembers.Add(selectedMember);
                }
            }

            // Check if any member is selected
            if (selectedMembers.Count == 0)
            {
                MessageBox.Show("Geen Leden Geselecteerd", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // Open ActivityWindow with the selected customer and members
            ActivityWindow activityWindow = new ActivityWindow(customerUI, selectedMembers);
            activityWindow.Show();
        }


    }
}
