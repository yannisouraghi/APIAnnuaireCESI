using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using ProjetAnnuaire.Model;
using Prism.Commands;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using ProjetAnnuaire.View;



namespace ProjetAnnuaire.ViewModel
{
    public class AjouterPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string firstName;
        private string lastName;
        private string department;
        private string email;
        private string phoneNumber;
        private string mobilePhone;
        private string jobTitle;
        private string jobDescription;

        private Services selectedService;
        private ObservableCollection<Services> services;

        private Sites selectedSite;
        private ObservableCollection<Sites> sites;

        public AjouterPageViewModel()
        {
            services = new ObservableCollection<Services>();
            sites = new ObservableCollection<Sites>();
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        public string Department
        {
            get { return department; }
            set
            {
                department = value;
                RaisePropertyChanged("Department");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }

        public string MobilePhone
        {
            get { return mobilePhone; }
            set
            {
                mobilePhone = value;
                RaisePropertyChanged("MobilePhone");
            }
        }

        public string JobTitle
        {
            get { return jobTitle; }
            set
            {
                jobTitle = value;
                RaisePropertyChanged("JobTitle");
            }
        }

        public string JobDescription
        {
            get { return jobDescription; }
            set
            {
                jobDescription = value;
                RaisePropertyChanged("JobDescription");
            }
        }

        public Services SelectedService
        {
            get { return selectedService; }
            set
            {
                selectedService = value;
                RaisePropertyChanged("SelectedService");
            }
        }

        public ObservableCollection<Services> Services
        {
            get { return services; }
            set
            {
                services = value;
                RaisePropertyChanged("Services");
            }
        }

        public Sites SelectedSite
        {
            get { return selectedSite; }
            set
            {
                selectedSite = value;
                RaisePropertyChanged("SelectedSite");
            }
        }

        public ObservableCollection<Sites> Sites
        {
            get { return sites; }
            set
            {
                sites = value;
                RaisePropertyChanged("Sites");
            }
        }

        private string newServiceName;

        public string NewServiceName
        {
            get { return newServiceName; }
            set
            {
                newServiceName = value;
                RaisePropertyChanged(nameof(NewServiceName));
            }
        }

        private string newSiteName;

        public string NewSiteName
        {
            get { return newSiteName; }
            set
            {
                newSiteName = value;
                RaisePropertyChanged(nameof(NewSiteName));
            }
        }

        private bool CanAjouter()
        {
            return true;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void AjouterService_Click()
        {
            try
            {
                CustomInputDialog dialog = new CustomInputDialog("Entrez le nom du service :");
                bool? result = dialog.ShowDialog();

                if (result == true && !string.IsNullOrEmpty(dialog.UserInput))
                {
                    NewServiceName = dialog.UserInput;

                    using (HttpClient client = new HttpClient())
                    {
                        string apiUrl = $"https://localhost:7053/api/Service/AddService";
                        Services newService = new Services { Service = NewServiceName };
                        string json = JsonConvert.SerializeObject(newService);
                        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Service ajouté avec succès !");
                            NewServiceName = string.Empty;
                        }
                        else
                        {
                            MessageBox.Show("Une erreur s'est produite lors de l'ajout du service.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}");
            }
        }

        public async void AjouterSite_Click()
        {
            try
            {
                CustomInputDialog dialog = new CustomInputDialog("Entrez le nom du site :");
                bool? result = dialog.ShowDialog();

                if (result == true && !string.IsNullOrEmpty(dialog.UserInput))
                {
                    NewServiceName = dialog.UserInput;

                    using (HttpClient client = new HttpClient())
                    {
                        string apiUrl = $"https://localhost:7053/api/Site/AddSite";
                        Sites newSite = new Sites { City = NewSiteName };
                        string json = JsonConvert.SerializeObject(newSite);
                        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Site ajouté avec succès !");
                            NewServiceName = string.Empty;
                        }
                        else
                        {
                            MessageBox.Show("Une erreur s'est produite lors de l'ajout du site.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}");
            }
        }

        public async void ModifierSite_Click()
        {
            MessageBox.Show("Modifier Site");
        }
        public async void SupprimerSite_Click()
        {
            MessageBox.Show("Supprimer Site");

        }
        public async void ModifierService_Click()
        {
            MessageBox.Show("Modifier Service");

        }
        public async void SupprimerService_Click()
        {
            MessageBox.Show("Supprimer Service");
        }
    }
}
