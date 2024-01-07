using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.CustomerWPF.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ManageMember.xaml
    /// </summary>
    public partial class ManageMember : Window
    {
        public MemberUI MemberUI { get; set; }
        private MemberManager memberManager;
        private readonly int? customerID;

        public ManageMember(MemberUI memberUI, int? customerID)
        {
            InitializeComponent();
            memberManager = new MemberManager(RepositoryFactory.MemberRepository);
            this.MemberUI = memberUI;
            this.customerID = customerID;

            if (memberUI != null )
            {
                CustomerIdTextBox.Text = Convert.ToString(memberUI.CustomerID);
                BirthdayPicker.Text = Convert.ToString(memberUI.Birthday);
                NameTextBox.Text = memberUI.Name;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            // Check if customerID is null and handle it
            if (!customerID.HasValue)
            {
                MessageBox.Show("Customer ID is not available.", "Error");
                return; // Exit the method if customerID is null
            }

            Customer customer = new Customer(customerID.Value); // Use .Value to get the int from int?

            Member newMember = new Member(NameTextBox.Text, DateOnly.FromDateTime(Convert.ToDateTime(BirthdayPicker.Text)), customer);

            if (MemberUI == null)
            {
                memberManager.AddMember(newMember);
            }
            else
            {
                Member member = new Member(MemberUI.Name, MemberUI.Birthday, customer);

                memberManager.UpdateMember(member, newMember);
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
