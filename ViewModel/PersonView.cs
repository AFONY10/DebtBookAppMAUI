using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using TheDebtBook.models;
using TheDebtBook.service;

namespace TheDebtBook.ViewModel
{
    public partial class PersonView : ObservableObject
    {
        private readonly TheDebtBookDatabase _database;
        private readonly Navigate _navigate;
        private readonly int _personId;

        [ObservableProperty] private string _name;

        [ObservableProperty] private string _amount;

        [ObservableProperty] private ObservableCollection<Balance> _balances;

        public PersonView(int personId)
        {
            Balances = new ObservableCollection<Balance>();
            _database = new TheDebtBookDatabase();
            _navigate = new Navigate();
            _ = Initialize();
            _ = SetupCollection();
        }

        private async Task Initialize()
        {
            var person = await _database.GetPerson(_personId);
            Name = person.Name;
        }

        private async Task SetupCollection()
        {
            Balances.Clear();
            var TBalances = await _database.GetAllBalancesOfPerson(_personId);
            foreach (var balance in TBalances)
            {
                Balances.Add(balance);
            }
        }

        [RelayCommand]
        public async Task DeletePerson()
        {
            var deleteNum = await _database.DeletePersonById(_personId);
            if (deleteNum <= 0) return;
            await _database.DeleteAllBalancesOfPersonId(_personId);
            _navigate.Return();
        }

        [RelayCommand]
        public async Task AddBalance()
        {
            var propertiesset = await CheckProperties();
            if (!propertiesset) return;

            var balance = new Balance(_personId, int.TryParse(Amount, out var bal) ? bal : 0);
            var deleteNum = await _database.AddBalance(balance);
            await SetupCollection();
        }

        [RelayCommand]
        public async Task DeleteBalance(Balance balance)
        {
            var deleteNum = await _database.DeleteBalance(balance);
            await SetupCollection();
        }

        private async Task<bool> CheckProperties()
        {
            if (string.IsNullOrEmpty(Amount) || Amount == "0")
            {
                await Application.Current.MainPage.DisplayAlert("Missing Input", "Amount has to be set", "OK");
                return false;
            }

            return true;
        }
    }
}
