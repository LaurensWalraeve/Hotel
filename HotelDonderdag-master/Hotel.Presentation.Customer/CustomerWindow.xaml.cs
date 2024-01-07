using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Customer.Model;
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

namespace Hotel.Presentation.Customers
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerUI CustomerUI { get; set; }
        private CustomerManager customerManager;
        public CustomerWindow(CustomerUI customerUI)
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            this.CustomerUI = customerUI;
            if (CustomerUI != null)
            {
                IdTextBox.Text = CustomerUI.Id.ToString();
                NameTextBox.Text = CustomerUI.Name;
                EmailTextBox.Text = CustomerUI.Email;
                PhoneTextBox.Text = CustomerUI.Phone;             
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerUI == null)
            {
                //Nieuw
                //wegschrijven
                //TODO nrofmembers
                Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);

                ContactInfo contactInfo = new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, address);

                Customer customer = new Customer(NameTextBox.Text, contactInfo);
                

                customerManager.AddCustomers(customer);
            }
            else
            {
                //Update
                //update DB
                CustomerUI.Email=EmailTextBox.Text;
                CustomerUI.Phone=PhoneTextBox.Text;
                CustomerUI.Name=NameTextBox.Text;
            }
            DialogResult = true;
            Close();
        }
    }
}
