using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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
using System.IO;
using System.Text.Json.Serialization;
using ProjetAnnuaire.ViewModel;
using ProjetAnnuaire.Model;

namespace ProjetAnnuaire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient;
        public UserViewModel CurrentUser { get; }

        public Visibility AdminExpanderVisibility
        {
            get { return (Visibility)GetValue(AdminExpanderVisibilityProperty); }
            set { SetValue(AdminExpanderVisibilityProperty, value); }
        }

        public static readonly DependencyProperty AdminExpanderVisibilityProperty =
            DependencyProperty.Register("AdminExpanderVisibility", typeof(Visibility), typeof(MainWindow), new PropertyMetadata(Visibility.Collapsed));



        public MainWindow()
        {
            InitializeComponent();
            contentFrame.Source = new Uri("RechercherPage.xaml", UriKind.Relative);
            this.Width = 900;
            this.Height = 700;
            _httpClient = new HttpClient();
            CommandBindings.Add(new CommandBinding(OpenAdminCommand, OpenAdminCommand_Executed, OpenAdminCommand_CanExecute));
            InputBindings.Add(new KeyBinding(OpenAdminCommand, new KeyGesture(Key.A, ModifierKeys.Control)));
            CurrentUser = new UserViewModel();
            DataContext = this;
        }

        public void NavigateToFicheEmployee(FicheEmployeeViewModel viewModel)
        {
            contentFrame.Navigate(new FicheEmployee(viewModel, true));
        }


        private void Deconnexion_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.IsAdmin = false;
            AdminExpanderVisibility = Visibility.Collapsed;
            NavigateToRechercherPage();
        }

        public void NavigateToRechercherPage()
        {
            contentFrame.Navigate(new Uri("View/RechercherPage.xaml", UriKind.Relative));
        }


        private void RechercherButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToRechercherPage();
        }

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new Uri("View/AjouterPage.xaml", UriKind.Relative));
        }

        private void SupprimerButton_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new Uri("View/SupprimerPage.xaml", UriKind.Relative));
        }

        public static RoutedCommand OpenAdminCommand = new RoutedCommand();

        private void OpenAdminCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenAdminWindow();
        }

        private void OpenAdminCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenAdminWindow()
        {
            PasswordDialog passwordWindow = new PasswordDialog();
            bool? result = passwordWindow.ShowDialog();

            if (result == true && passwordWindow.EnteredPassword == "admin")
            {
                // Mettre à jour la propriété IsAdmin
                CurrentUser.IsAdmin = true;

                // Mettre à jour la visibilité de l'expander
                AdminExpanderVisibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Mot de passe incorrect");
            }
        }

    }
}
