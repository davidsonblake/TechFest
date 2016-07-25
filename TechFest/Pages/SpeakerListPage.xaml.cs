using Xamarin.Forms;

namespace TechFest.Pages
{
    public partial class SpeakerListPage : ContentPage
    {
        public SpeakerListPage()
        {
            InitializeComponent();
        }

		void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;

			lstSpeakers.SelectedItem = null;
		}

		void Handle_Refreshing(object sender, System.EventArgs e)
		{
			lstSpeakers.IsRefreshing = false;
		}
	}
}