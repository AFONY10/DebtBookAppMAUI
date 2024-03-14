using TheDebtBook.ViewModel;

namespace TheDebtBook.Views
{
    public partial class AddSite : ContentPage
    {
        private readonly AddPersonView _viewModel;

        public AddSite()
        {
            InitializeComponent();
            _viewModel = new AddPersonView();
            BindingContext = _viewModel;
        }
    }
}