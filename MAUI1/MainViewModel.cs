using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI1
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));
                    FilterContacts();
                }
            }
        }

        private ObservableCollection<Contact> _contacts;
        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value;
                OnPropertyChanged(nameof(Contacts));
            }
        }

        private ObservableCollection<Contact> _allContacts;

        public MainViewModel()
        {
            _allContacts = new ObservableCollection<Contact>
            {
                new Contact { Name = "Ali Veli", PhoneNumber = "555-123-4567" },
                new Contact { Name = "Ayşe Yılmaz", PhoneNumber = "555-987-6543" },
                new Contact { Name = "Mehmet Demir", PhoneNumber = "555-456-7890" }
            };

            Contacts = new ObservableCollection<Contact>(_allContacts);
        }

        private void FilterContacts()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Contacts = new ObservableCollection<Contact>(_allContacts);
            }
            else
            {
                Contacts = new ObservableCollection<Contact>(
                    _allContacts.Where(c => c.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
