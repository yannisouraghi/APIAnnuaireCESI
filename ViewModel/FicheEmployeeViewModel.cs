using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using ProjetAnnuaire.Model;

namespace ProjetAnnuaire.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();
    }

    public class FicheEmployeeViewModel : INotifyPropertyChanged
    {
        private string lastName;
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

        private string firstName;
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

        private string jobTitle;
        public string JobTitle
        {
            get { return jobTitle; }
            set
            {
                if (jobTitle != value)
                {
                    jobTitle = value;
                    OnPropertyChanged(nameof(JobTitle));
                }
            }
        }

        private string jobDescription;
        public string JobDescription
        {
            get { return jobDescription; }
            set
            {
                if (jobDescription != value)
                {
                    jobDescription = value;
                    OnPropertyChanged(nameof(JobDescription));
                }
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                if (phoneNumber != value)
                {
                    phoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }

        private string mobilePhone;
        public string MobilePhone
        {
            get { return mobilePhone; }
            set
            {
                if (mobilePhone != value)
                {
                    mobilePhone = value;
                    OnPropertyChanged(nameof(MobilePhone));
                }
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        private string service;

        public string Service
        {
            get { return service; }
            set
            {
                if (service != value)
                {
                    service = value;
                    OnPropertyChanged(nameof(Service));
                }
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                {
                    city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        private bool isAdmin;
        public bool IsAdmin
        {
            get { return isAdmin; }
            set
            {
                Debug.WriteLine(isAdmin);
                if (isAdmin != value)
                {
                    isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));

                }
            }
        }

        public ICommand ModifyLastNameCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public FicheEmployeeViewModel()
        {
            ModifyLastNameCommand = new RelayCommand(ModifyLastName, CanModifyLastName);
        }

        private void ModifyLastName()
        {
            // Appel de l'API de mise à jour avec le nouveau LastName
            // Utilisez la propriété LastName pour obtenir la nouvelle valeur
            // N'oubliez pas de mettre à jour la propriété LastName après la modification réussie
        }

        private bool CanModifyLastName()
        {
            // Ajoutez la logique pour déterminer si l'utilisateur actuel peut modifier le champ LastName
            // Par exemple, vérifiez si IsAdmin est vrai
            return isAdmin;
        }

        public void setIsAdmin(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }
    }
}
