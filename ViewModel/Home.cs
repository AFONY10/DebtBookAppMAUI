using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using TheDebtBook.models;
using TheDebtBook.service;
using TheDebtBook.data;


namespace TheDebtBook.ViewModel
{
    public partial class Home : ObservableObject
    {
        private readonly TheDebtBookDatabase _database;
        private readonly Navigate _navigate;

        [ObservableProperty]
        private ObservableCollection<AllPerson> _people;

        public Home()
        {
            People = new ObservableCollection<AllPerson>();
            _database = new TheDebtBookDatabase();
            _navigate = new Navigate();
        }

        [RelayCommand]
        private async Task SetupCollection()
        {
            People.Clear();

            var TPeople = await _database.GetAllPeople();
            foreach (var person in TPeople)
            {
                var allBalances = await _database.GetAllBalancesOfPerson(person.Id);
                int totalBalance = 0;

                foreach (var balance in allBalances)
                {
                    totalBalance += balance.Amount;
                }

                People.Add(new AllPerson(person, totalBalance));
            }
        }

        [RelayCommand]
        public async Task NavigateToPerson(int personId)
        {
            await _navigate.ToPersonSite(personId);
        }

        [RelayCommand]
        public async Task NavigateToAddPerson()
        {
            await _navigate.ToAddSite();
        }
    }
}
