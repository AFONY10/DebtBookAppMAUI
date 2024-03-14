using TheDebtBook.ViewModel;
using TheDebtBook.Views;

namespace TheDebtBook.service
{
    public class Navigate
    {
        public async Task ToPersonSite(int personId)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PersonSite(personId));
        }

        public async Task ToAddSite()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddSite());
        }

        public async Task ToAddHome(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Page());
        }

        public async Task Return()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
