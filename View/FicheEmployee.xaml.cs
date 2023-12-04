using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ProjetAnnuaire.Model;
using ProjetAnnuaire.ViewModel;

namespace ProjetAnnuaire
{
    public partial class FicheEmployee : Page
    {
        // Propriété pour le ViewModel
        public FicheEmployeeViewModel ViewModel { get; set; }

        public FicheEmployee(FicheEmployeeViewModel viewModel, bool isAdmin)
        {
            InitializeComponent();
            ViewModel = viewModel;
            ViewModel.setIsAdmin(true);
            DataContext = ViewModel;
        }


        public void Retour_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.NavigateToRechercherPage();
            }
        }
    }
}
