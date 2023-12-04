using ProjetAnnuaire.Model;
using ProjetAnnuaire.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjetAnnuaire
{
    public partial class SupprimerPage : Page
    {
        public SupprimerPage()
        {
            InitializeComponent();
            DataContext = new SupprimerPageViewModel();
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is SupprimerPageViewModel viewModel)
            {
                viewModel.SearchAsync();
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is  SupprimerPageViewModel viewModel)
            {
                viewModel.DeleteSelectedEmployees();
            }
        }

        private void DataGridDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedEmployee = (Employees)EmployeeDataGrid.SelectedItem;

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
