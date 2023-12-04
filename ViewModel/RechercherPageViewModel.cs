using ProjetAnnuaire.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjetAnnuaire.Model;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace ProjetAnnuaire.ViewModel
{
    public class RechercherPageViewModel : INotifyPropertyChanged
    {

        public ICommand SearchCommand { get; set; }

        public RechercherPageViewModel()
        {
            // Initialisez votre commande ici
            SearchCommand = new RelayCommand(async () => await SearchAsync(), () => CanExecuteSearchCommand());
        }

        private bool CanExecuteSearchCommand()
        {
            // Ajoutez votre logique ici (par exemple, vérifiez si les conditions pour exécuter la recherche sont remplies)
            return true;
        }

        private ObservableCollection<Employees> _employeesList;
        public ObservableCollection<Employees> EmployeesList
        {
            get { return _employeesList; }
            set
            {
                _employeesList = value;
                OnPropertyChanged(nameof(EmployeesList));
            }
        }

        private ObservableCollection<Services> _servicesList;
        public ObservableCollection<Services> ServicesList
        {
            get { return _servicesList; }
            set
            {
                _servicesList = value;
                OnPropertyChanged(nameof(ServicesList));
            }
        }

        private ObservableCollection<Sites> _sitesList;
        public ObservableCollection<Sites> SitesList
        {
            get { return _sitesList; }
            set
            {
                _sitesList = value;
                OnPropertyChanged(nameof(SitesList));
            }
        }


        private string _searchByName;
        public string SearchByName
        {
            get { return _searchByName; }
            set
            {
                _searchByName = value;
                OnPropertyChanged(nameof(SearchByName));
            }
        }

        private Services _selectedService;
        public Services SelectedService
        {
            get { return _selectedService; }
            set
            {
                _selectedService = value;
                OnPropertyChanged(nameof(SelectedService));
            }
        }

        private Sites _selectedSite;
        public Sites SelectedSite
        {
            get { return _selectedSite; }
            set
            {
                _selectedSite = value;
                OnPropertyChanged(nameof(SelectedSite));
            }
        }

        public async Task SearchAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                var baseUri = "https://localhost:7053";
                var uri = $"{baseUri}/api/Employee/Search";
                var queryParams = new List<string>();

                if (!string.IsNullOrEmpty(SearchByName))
                    queryParams.Add($"name={Uri.EscapeDataString(SearchByName)}");

                if (SelectedSite != null)
                    queryParams.Add($"site={Uri.EscapeDataString(SelectedSite.City)}");

                if (SelectedService != null)
                    queryParams.Add($"service={Uri.EscapeDataString(SelectedService.Service)}");

                if (queryParams.Count > 0)
                    uri += "?" + string.Join("&", queryParams);

                var response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var employees = JsonConvert.DeserializeObject<List<Employees>>(jsonResult);

                    // Afficher le service de chaque employé
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"Employee ID: {employee.EmployeeId}, Service: {employee.Service}");
                    }

                    EmployeesList = new ObservableCollection<Employees>(employees);
                }
                else
                {
                    // Gérer les erreurs
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs
            }
        }


        public async Task InitializeServicesAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                var baseUri = "https://localhost:7053";
                var uri = $"{baseUri}/api/Service/Services";

                var response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var services = JsonConvert.DeserializeObject<List<Services>>(jsonResult);
                    ServicesList = new ObservableCollection<Services>(services);
                }
                else
                {
                    // Gérer les erreurs
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs
            }
        }

        public async Task InitializeSitesAsync()
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
                    var sites = JsonConvert.DeserializeObject<List<Sites>>(jsonResult);
                    SitesList = new ObservableCollection<Sites>(sites);
                }
                else
                {
                    // Gérer les erreurs
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
