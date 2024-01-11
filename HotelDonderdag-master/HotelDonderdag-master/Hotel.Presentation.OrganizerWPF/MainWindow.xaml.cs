using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.OrganizerWPF.Model;
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

namespace Hotel.Presentation.OrganizerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<OrganizerUI> organizerUIs = new ObservableCollection<OrganizerUI>();
        private OrganizerManager organizerManager;
        public MainWindow()
        {
            InitializeComponent();
            organizerManager = new OrganizerManager(RepositoryFactory.OrganizerRepository);
            organizerUIs = new ObservableCollection<OrganizerUI>(organizerManager.GetOrganizers(null).Select(x => new OrganizerUI(x.OrganizerID, x.Name, x.Email, x.Phone, x.Address.ToString())));
            OrganizerDataGrid.ItemsSource = organizerUIs;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            organizerUIs = new ObservableCollection<OrganizerUI>(organizerManager.GetOrganizers(SearchTextBox.Text).Select(x => new OrganizerUI(x.OrganizerID, x.Name, x.Email, x.Phone, x.Address.ToString())));
            OrganizerDataGrid.ItemsSource = organizerUIs;
        }

        private void MenuItemAddOrganizer_Click(object sender, RoutedEventArgs e)
        {
            OrganizerWindow w = new OrganizerWindow(null);
            if (w.ShowDialog() == true)
                organizerUIs = new ObservableCollection<OrganizerUI>(organizerManager.GetOrganizers(null).Select(x => new OrganizerUI(x.OrganizerID, x.Name, x.Email, x.Phone, x.Address.ToString())));
                OrganizerDataGrid.ItemsSource = organizerUIs;

        }
        private void MenuItemDeleteOrganizer_Click(object sender, RoutedEventArgs e)
        {
            if (OrganizerDataGrid.SelectedItem == null) MessageBox.Show("not selected", "delete");
            else
            {
                OrganizerUI organizerUI = (OrganizerUI)OrganizerDataGrid.SelectedItem;



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

                organizerManager.DeleteOrganizer(organizer);

                organizerUIs = new ObservableCollection<OrganizerUI>(organizerManager.GetOrganizers(null).Select(x => new OrganizerUI(x.OrganizerID, x.Name, x.Email, x.Phone, x.Address.ToString())));
                OrganizerDataGrid.ItemsSource = organizerUIs;
            }
        }
        private void MenuItemUpdateOrganizer_Click(object sender, RoutedEventArgs e)
        {
            if (OrganizerDataGrid.SelectedItem == null) MessageBox.Show("not selected", "update");
            else
            {
                OrganizerWindow w = new OrganizerWindow((OrganizerUI)OrganizerDataGrid.SelectedItem);
                if (w.ShowDialog() == true)
                    organizerUIs = new ObservableCollection<OrganizerUI>(organizerManager.GetOrganizers(null).Select(x => new OrganizerUI(x.OrganizerID, x.Name, x.Email, x.Phone, x.Address.ToString())));
                    OrganizerDataGrid.ItemsSource = organizerUIs;
            }
        }
        private void MenuItemGetActivities_Click(object sender, RoutedEventArgs e)
        {
            if (OrganizerDataGrid.SelectedItem == null) MessageBox.Show("not selected", "Organizer");
            else
            {
                OrganizerUI organizerUI = (OrganizerUI)OrganizerDataGrid.SelectedItem;


                ActivityWindow w = new ActivityWindow(organizerUI);
                if (w.ShowDialog() == true)
                    organizerUIs = new ObservableCollection<OrganizerUI>(organizerManager.GetOrganizers(null).Select(x => new OrganizerUI(x.OrganizerID, x.Name, x.Email, x.Phone, x.Address.ToString())));
                    OrganizerDataGrid.ItemsSource = organizerUIs;
            }
        }

    }
}