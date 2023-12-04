using ProjetAnnuaire.Model;
using ProjetAnnuaire.ViewModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Navigation;

namespace ProjetAnnuaire
{
    public partial class RechercherPage : Page
    {
        public RechercherPageViewModel ViewModel { get; set; }

        public RechercherPage()
        {
            InitializeComponent();
            ViewModel = new RechercherPageViewModel();
            DataContext = ViewModel;
            InitializeServicesComboBoxAsync();
            InitializeSitesComboBoxAsync();  // Ajout de l'initialisation des sites
        }


        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SearchAsync();
        }

        private async Task InitializeServicesComboBoxAsync()
        {
            await ViewModel.InitializeServicesAsync();
        }

        private async Task InitializeSitesComboBoxAsync()
        {
            await ViewModel.InitializeSitesAsync();
        }

        private void EffacerButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedService = null;
            ViewModel.SelectedSite = null;
            ViewModel.SearchByName = string.Empty;
        }

        private void DataGridDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedEmployee = (Employees)EmployeesData.SelectedItem;

            if (selectedEmployee != null)
            {
                var ficheEmployeeViewModel = new FicheEmployeeViewModel
                {
                    LastName = selectedEmployee.LastName,
                    FirstName = selectedEmployee.FirstName,
                    JobTitle = selectedEmployee.JobTitle,
                    JobDescription = selectedEmployee.JobDescription,
                    PhoneNumber = selectedEmployee.PhoneNumber,
                    MobilePhone = selectedEmployee.MobilePhone,
                    Email = selectedEmployee.Email,
                    Service = selectedEmployee.Service,
                    City = selectedEmployee.City
                };

                var mainWindow = Application.Current.MainWindow as MainWindow;

                if (mainWindow != null)
                {
                    mainWindow.NavigateToFicheEmployee(ficheEmployeeViewModel);
                }
            }
        }




    }
}
