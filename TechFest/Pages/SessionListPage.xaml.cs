using TechFest.PageModels;
using Xamarin.Forms;

namespace TechFest.Pages
{
    public partial class SessionListPage : ContentPage
    {
        public SessionListPage()
        {
            InitializeComponent();
        }

        private void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            lstSessions.SelectedItem = null;
            ((SessionListPageModel)BindingContext).SessionSelected.Execute(e.SelectedItem);
        }
    }
}