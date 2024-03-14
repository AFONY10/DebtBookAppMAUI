using TheDebtBook.ViewModel;

namespace TheDebtBook.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly Home _viewModel;
        public MainPage()
        {
            InitializeComponent();
            _viewModel = new Home();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.SetupCollectionCommand.Execute(null);
        }

 
    }

}
