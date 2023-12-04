using Newtonsoft.Json;
using ProjetAnnuaire.Model;
using ProjetAnnuaire.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjetAnnuaire
{
    /// <summary>
    /// Logique d'interaction pour AjouterPage.xaml
    /// </summary>
    public partial class AjouterPage : Page
    {

        public AjouterPage()
        {
            InitializeComponent();
            DataContext = new AjouterPageViewModel();
            Loaded += AjouterPage_Loaded;
        }

        private async void AjouterPage_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeServicesComboBoxAsync();
            await InitializeSiteComboBoxAsync();
        }

        private async void AjouterService_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AjouterPageViewModel viewModel)
            {
                viewModel.AjouterService_Click();
            }
        }

        private async void ModifierService_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AjouterPageViewModel viewModel)
            {
                viewModel.ModifierService_Click();
            }
        }

        private async void SupprimerService_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AjouterPageViewModel viewModel)
            {
                viewModel.SupprimerService_Click();
            }
        }


        private async void AjouterSite_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AjouterPageViewModel viewModel)
            {
                viewModel.AjouterSite_Click();
            }
        }

        private async void ModifierSite_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AjouterPageViewModel viewModel)
            {
                viewModel.ModifierSite_Click();
            }
        }

        private async void SupprimerSite_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AjouterPageViewModel viewModel)
            {
                viewModel.SupprimerSite_Click();
            }
        }

        public async void Ajouter_Click(object sender, RoutedEventArgs e)
{
    try
    {
        // Récupérez les données saisies par l'utilisateur
        CreateEmployeeModel employeeModel = new CreateEmployeeModel
        {
            FirstName = FirstNameTextBox.Text,
            LastName = LastNameTextBox.Text,
            Department = DepartmentTextBox.Text,
            Email = EmailTextBox.Text,
            PhoneNumber = PhoneTextBox.Text,
            MobilePhone = MobileTextBox.Text,
            JobTitle = JobTitleTextBox.Text,
            JobDescription = JobDescriptionTextBox.Text,
            ServiceId = (Service.SelectedItem as Services)?.ServiceId ?? 0,
            SiteId = (Site.SelectedItem as Sites)?.SiteId ?? 0
        };

        // Convertissez l'objet en JSON
        string jsonEmployee = JsonConvert.SerializeObject(employeeModel);

        // Définissez l'URL de l'API
        string apiUrl = "https://localhost:7053/api/Employee/AddEmployee";

        // Utilisez HttpClient pour envoyer les données au serveur
        using (HttpClient client = new HttpClient())
        {
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Employé ajouté avec succès !");
            }
            else
            {
                MessageBox.Show("Une erreur s'est produite lors de l'ajout de l'employé.");
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Une erreur s'est produite : {ex.Message}");
    }
}



        private async Task InitializeSiteComboBoxAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                var baseUri = "https://localhost:7053";
                var uri = $"{baseUri}/api/Site/Sites";

                var response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var site = JsonConvert.DeserializeObject<List<Sites>>(jsonResult);
                    Site.ItemsSource = site;
                }
                else
                {
                    MessageBox.Show($"Erreur lors de la requête API : {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private async Task InitializeServicesComboBoxAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                var baseUri = "https://localhost:7053";
                var uri = $"{baseUri}/api/Service/Services"; // Assurez-vous que l'URL est correcte

                var response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var services = JsonConvert.DeserializeObject<List<Services>>(jsonResult);
                    Service.ItemsSource = services;
                }
                else
                {
                    MessageBox.Show($"Erreur lors de la requête API : {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }
    }
}
