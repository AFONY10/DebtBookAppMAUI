using TheDebtBook.ViewModel;

namespace TheDebtBook.Views
{
    public partial class PersonSite : ContentPage
    {
        private readonly PersonView _viewModel;

        public PersonSite(int personId)
        {
            InitializeComponent();
            _viewModel = new PersonView(personId);
            BindingContext = _viewModel;
        }
    }
}