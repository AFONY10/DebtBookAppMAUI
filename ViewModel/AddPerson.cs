using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using TheDebtBook.models;
using TheDebtBook.service;

namespace TheDebtBook.ViewModel
{
    public partial class AddPersonView : ObservableObject
    {
        private readonly TheDebtBookDatabase _database;
        private readonly Navigate _navigation;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _creditorDebtor;

        [ObservableProperty]
        private string _initialBalance;

        public AddPersonView()
        {
            _database = new TheDebtBookDatabase();
            _navigation = new Navigate();
        }

        [RelayCommand]
        public void SetDebtor() => CreditorDebtor = "Debtor";

        [RelayCommand]
        public void SetCreditor() => CreditorDebtor = "Creditor";

        [RelayCommand]
        public async Task AddPerson()
        {
            var propertiesAreSet = await CheckProperties();
            if (!propertiesAreSet) return;

            var person = new Person(CreditorDebtor, Name);
            var insertedPerson = await _database.AddPerson(person);
            if (insertedPerson == 0) return;

            var personId = await _database.GetIdOfLastPerson();
            if (personId == -1) return;

            var balance = new Balance(personId, int.TryParse(InitialBalance, out var parsedValue) ? parsedValue : 0);
            var insertedBalance = await _database.AddBalance(balance);
            if (insertedBalance == 0) return;

            Name = CreditorDebtor = InitialBalance = string.Empty;

            _navigation.Return();
        }

        private async Task<bool> CheckProperties()
        {
            if (string.IsNullOrEmpty(CreditorDebtor))
            {
                await Application.Current.MainPage.DisplayAlert("Missing Input", "Either Creditor or Debtor must be chosen", "OK");
                return false;
            }
            if (string.IsNullOrEmpty(Name))
            {
                await Application.Current.MainPage.DisplayAlert("Missing Input", "Name must be chosen", "OK");
                return false;
            }
            if (string.IsNullOrEmpty(InitialBalance) || InitialBalance == "0")
            {
                await Application.Current.MainPage.DisplayAlert("Missing Input", "Initial balance must be chosen", "OK");
                return false;
            }

            return true;
        }
    }
}
