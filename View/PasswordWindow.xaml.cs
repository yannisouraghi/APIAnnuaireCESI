using System.Windows;

namespace ProjetAnnuaire
{
    public partial class PasswordDialog : Window
    {
        public string EnteredPassword { get; private set; }

        public PasswordDialog()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            EnteredPassword = passwordBox.Password;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
