using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.CustomerWPF.Model;
using Hotel.Util;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel.Presentation.CustomerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<CustomerUI> customerUIs = new ObservableCollection<CustomerUI>();
        private CustomerManager customerManager;
        //private string conn = "Data Source=NB21-6CDPYD3\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True";
        public MainWindow()
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            customerUIs = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(null).Select(x => new CustomerUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone)));
            CustomerDataGrid.ItemsSource = customerUIs;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            customerUIs = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(SearchTextBox.Text).Select(x => new CustomerUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone)));
            CustomerDataGrid.ItemsSource = customerUIs;
        }

        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow w = new CustomerWindow(null);
            if (w.ShowDialog() == true)
                customerUIs = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(null).Select(x => new CustomerUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone)));
                CustomerDataGrid.ItemsSource = customerUIs;
        }
        private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null) MessageBox.Show("not selected", "delete");
            else
            {
                CustomerUI customerUI = (CustomerUI)CustomerDataGrid.SelectedItem;

                //address
                // Splitting the address into parts
                string[] parts = customerUI.Address.Split(new[] { " - " }, StringSplitOptions.None);

                // Extracting city and zip code
                string cityAndZip = parts[0];
                string city = cityAndZip.Split('[')[0].Trim();
                string zip = cityAndZip.Split('[')[1].Trim(']');

                // Extracting street and house number
                string street = parts[1];
                string houseNumber = parts[2];


                Address address = new Address(city, street, zip, houseNumber);

                ContactInfo contactInfo = new ContactInfo(customerUI.Email, customerUI.Phone, address);

                Customer customer = new Customer(Convert.ToInt32(customerUI.Id), customerUI.Name, contactInfo);

                customerManager.DeleteCustomer(customer);

                customerUIs = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(null).Select(x => new CustomerUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone)));
                CustomerDataGrid.ItemsSource = customerUIs;
            }



            
        }
        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null) MessageBox.Show("not selected", "update");
            else
            {
                CustomerWindow w = new CustomerWindow((CustomerUI)CustomerDataGrid.SelectedItem);
                if (w.ShowDialog() == true)
                    customerUIs = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(null).Select(x => new CustomerUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone)));
                    CustomerDataGrid.ItemsSource = customerUIs;
            }
        }

        private void MenuItemGetMembers_Click(object sender, RoutedEventArgs e)
        {
            
            if (CustomerDataGrid.SelectedItem == null) MessageBox.Show("not selected", "Customer");
            else
            {
                CustomerUI customerUI = (CustomerUI)CustomerDataGrid.SelectedItem;


                MemberWindow w = new MemberWindow(customerUI);
                if (w.ShowDialog() == true)
                    customerUIs = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(null).Select(x => new CustomerUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone)));
                    CustomerDataGrid.ItemsSource = customerUIs;
            }
        }
    }
}