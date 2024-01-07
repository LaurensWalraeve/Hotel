using Hotel.Domain.Managers;
using Hotel.Domain.Model;
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

namespace Hotel.Presentation.CustomerWPF
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
            MemberDataGrid.ItemsSource = memberUIs; // Set the ItemsSource
            CustomerInfo.Text = customerUI.Name; // Update customer info
        }

        private void MenuItemAddMember_Click(object sender, RoutedEventArgs e)
        {
            ManageMember w = new ManageMember(null, customerUI.Id);
            w.Closed += (s, args) => LoadMembers(); // Refresh members list when ManageMember window closes
            w.ShowDialog();
        }

        private void MenuItemDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            if (MemberDataGrid.SelectedItem == null) MessageBox.Show("not selected", "delete");
            else
            {
                MemberUI memberUI = (MemberUI)MemberDataGrid.SelectedItem;

                Customer customer = new Customer(memberUI.CustomerID);

                Member member = new Member(memberUI.Name, memberUI.Birthday, customer);

                memberManager.DeleteMember(member);

                LoadMembers();
            }
        }

        private void MenuItemUpdateMember_Click(object sender, RoutedEventArgs e)
        {
            

            if (MemberDataGrid.SelectedItem == null) MessageBox.Show("not selected", "update");
            else
            {
                MemberUI memberUI = (MemberUI)MemberDataGrid.SelectedItem;

                ManageMember w = new ManageMember(memberUI, customerUI.Id);
                w.Closed += (s, args) => LoadMembers(); // Refresh members list when ManageMember window closes
                w.ShowDialog();
            }
        }
    }
}
