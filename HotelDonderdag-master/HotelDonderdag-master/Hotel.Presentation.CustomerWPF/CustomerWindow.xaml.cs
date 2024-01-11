using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.CustomerWPF.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

                //address
                // Splitting the address into parts
                string[] parts = CustomerUI.Address.Split(new[] { " - " }, StringSplitOptions.None);

                // Extracting city and zip code
                string cityAndZip = parts[0];
                string city = cityAndZip.Split('[')[0].Trim();
                string zip = cityAndZip.Split('[')[1].Trim(']');

                // Extracting street and house number
                string street = parts[1];
                string houseNumber = parts[2];

                CityTextBox.Text = city;
                ZipTextBox.Text = zip;
                StreetTextBox.Text = street;
                HouseNumberTextBox.Text = houseNumber;



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
                Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);

                ContactInfo contactInfo = new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, address);

                Customer customer = new Customer(NameTextBox.Text, contactInfo);

                customerManager.AddCustomers(customer);
            }
            else
            {
                Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);

                ContactInfo contactInfo = new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, address);

                Customer customer = new Customer(Convert.ToInt32(IdTextBox.Text), NameTextBox.Text, contactInfo);

                customerManager.UpdateCustomers(customer);


            }
            DialogResult = true;
            Close();
        }
    }
}
