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

namespace ProjetAnnuaire.View
{
    /// <summary>
    /// Logique d'interaction pour CustomInputDialog.xaml
    /// </summary>
    public partial class CustomInputDialog : Window
    {
        public string UserInput { get; private set; }

        public CustomInputDialog(string prompt)
        {
            InitializeComponent();

            TextBlock promptTextBlock = new TextBlock { Text = prompt };
            TextBox userInputTextBox = new TextBox();

            Button okButton = new Button { Content = "OK", IsDefault = true };
            okButton.Click += (sender, e) =>
            {
                UserInput = userInputTextBox.Text;
                DialogResult = true;
            };

            Button cancelButton = new Button { Content = "Annuler", IsCancel = true };
            cancelButton.Click += (sender, e) => DialogResult = false;

            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(promptTextBlock);
            stackPanel.Children.Add(userInputTextBox);
            stackPanel.Children.Add(okButton);
            stackPanel.Children.Add(cancelButton);

            Content = stackPanel;
        }
    }
}
