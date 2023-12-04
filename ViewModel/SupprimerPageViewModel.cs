using ProjetAnnuaire.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;

namespace ProjetAnnuaire.ViewModel
{
    public class SupprimerPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Commande pour la recherche
        public ICommand SearchCommand { get; set; }

        // Commande pour supprimer les employés sélectionnés
        public ICommand DeleteSelectedEmployeesCommand { get; set; }

        // Propriétés bindables pour les filtres de recherche
        private string lastName;
        private string firstName;

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        // Liste des employés à afficher dans le DataGrid
        private ObservableCollection<Employees> employeesList;

        public ObservableCollection<Employees> EmployeesList
        {
            get { return employeesList; }
            set
            {
                if (employeesList != value)
                {
                    employeesList = value;
                    OnPropertyChanged(nameof(EmployeesList));
                }
            }
        }

        // Propriété pour gérer la sélection dans le DataGrid
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        // Méthode asynchrone pour effectuer la recherche
        public async Task SearchAsync()
        {
            try
            {
                // Configurer le client HTTP
                var httpClient = new HttpClient();
                var baseUri = "https://localhost:7053";
                var uri = $"{baseUri}/api/Employee/Search";
                var queryParams = new List<string>();

                // Ajouter les paramètres de recherche s'ils sont spécifiés
                if (!string.IsNullOrEmpty(LastName))
                    queryParams.Add($"Lastname={Uri.EscapeDataString(LastName)}");

                if (!string.IsNullOrEmpty(FirstName))
                    queryParams.Add($"Firstname={Uri.EscapeDataString(FirstName)}");

                if (queryParams.Count > 0)
                    uri += "?" + string.Join("&", queryParams);

                // Effectuer la requête HTTP GET
                var response = await httpClient.GetAsync(uri);

                // Traiter la réponse
                if (response.IsSuccessStatusCode)
                {
                    // Lire le contenu JSON de la réponse
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    // Désérialiser le JSON en une liste d'employés
                    var employees = JsonConvert.DeserializeObject<List<Employees>>(jsonResult);

                    // Mettre à jour la liste des employés dans le DataGrid
                    EmployeesList = new ObservableCollection<Employees>(employees);
                }
                else
                {
                    // Gérer les erreurs liées à la requête
                    MessageBox.Show($"Erreur lors de la recherche : {response.ReasonPhrase}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs générales
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Méthode pour supprimer les employés sélectionnés
        public void DeleteSelectedEmployees()
        {
            try
            {
                // Obtenir les employés sélectionnés
                var selectedEmployees = EmployeesList.Where(e => e.IsSelected).ToList();

                // Demander confirmation à l'utilisateur
                MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer les employés sélectionnés ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Traiter la réponse de l'utilisateur
                if (result == MessageBoxResult.Yes)
                {
                    // Supprimer les employés
                    foreach (var selectedEmployee in selectedEmployees)
                    {
                        DeleteEmployee(selectedEmployee.EmployeeId);
                    }

                    // Rafraîchir la liste des employés après la suppression
                    SearchAsync();
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs
                MessageBox.Show($"Une erreur s'est produite lors de la suppression : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Méthode pour déterminer si la suppression est possible
        private bool CanDeleteSelectedEmployees(object obj)
        {
            // Vérifier si au moins un employé est sélectionné
            return EmployeesList.Any(e => e.IsSelected);
        }

        // Méthode pour supprimer un employé par son ID
        private async Task DeleteEmployee(int employeeId)
        {
            try
            {
                // Configurer le client HTTP
                using (var client = new HttpClient())
                {
                    var baseUri = "https://localhost:7053";
                    var uri = $"{baseUri}/api/Employee/{employeeId}";

                    // Effectuer la requête HTTP DELETE
                    var response = await client.DeleteAsync(uri);

                    // Traiter la réponse
                    if (!response.IsSuccessStatusCode)
                    {
                        // Gérer les erreurs liées à la suppression
                        MessageBox.Show($"Erreur lors de la suppression de l'employé {employeeId} : {response.ReasonPhrase}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs générales
                MessageBox.Show($"Une erreur s'est produite lors de la suppression : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
