using TechFest.PageModels;
using Xamarin.Forms;

namespace TechFest.Pages
{
    public partial class EventListPage : ContentPage
    {
        public EventListPage()
        {
            InitializeComponent();
        }

		void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;

			lstEvents.SelectedItem = null;

			((EventListPageModel)BindingContext).EventSelected.Execute(e.SelectedItem);
		}

		void Handle_Refreshing(object sender, System.EventArgs e)
		{
			lstEvents.IsRefreshing = false;
		}

		public void HideBackgroundImage()
		{
			imgMain.IsVisible = false;
		}
	}
}