using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.OrganizerWPF.Model;
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

namespace Hotel.Presentation.OrganizerWPF
{
    /// <summary>
    /// Interaction logic for OrganizerWindow.xaml
    /// </summary>
    /// 

    

    public partial class OrganizerWindow : Window
    {

        public OrganizerUI OrganizerUI { get; set; }

        private OrganizerManager organizerManager;
        public OrganizerWindow(OrganizerUI organizerUI)
        {
            InitializeComponent();
            organizerManager = new OrganizerManager(RepositoryFactory.OrganizerRepository);
            this.OrganizerUI = organizerUI;

            if (organizerUI != null )
            {
                IdTextBox.Text = OrganizerUI.OrganizerID.ToString();
                NameTextBox.Text = OrganizerUI.Name;
                EmailTextBox.Text = OrganizerUI.Email;
                PhoneTextBox.Text = OrganizerUI.Phone;

                //address
                // Splitting the address into parts
                string[] parts = OrganizerUI.Address.Split(new[] { " - " }, StringSplitOptions.None);

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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrganizerUI == null)
            {
                Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);
                Organizer organizer = new Organizer(NameTextBox.Text, EmailTextBox.Text, PhoneTextBox.Text, address);

                organizerManager.AddOrganizer(organizer);
            }
            else
            {
                Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);
                Organizer organizer = new Organizer(Convert.ToInt32(IdTextBox.Text), NameTextBox.Text, EmailTextBox.Text, PhoneTextBox.Text, address);

                organizerManager.UpdateOrganizer(organizer);
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
