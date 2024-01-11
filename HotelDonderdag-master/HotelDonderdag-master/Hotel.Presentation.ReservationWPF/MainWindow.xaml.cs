using Hotel.Domain.Managers;
using Hotel.Presentation.CustomerWPF;
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

namespace Hotel.Presentation.ReservationWPF
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

        private void ChooseCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if a customer is selected
            if (CustomerDataGrid.SelectedItem is CustomerUI selectedCustomer)
            {
                // Open MemberWindow with the selected customer's information
                MemberWindow memberWindow = new MemberWindow(selectedCustomer);
                memberWindow.Show();
            }
            else
            {
                MessageBox.Show("Selecteer eerst een klant.", "Klant Niet Geselecteerd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            customerUIs = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(SearchTextBox.Text).Select(x => new CustomerUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone)));
            CustomerDataGrid.ItemsSource = customerUIs;
        }
    }
}