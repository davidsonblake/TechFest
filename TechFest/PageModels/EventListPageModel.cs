using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreshMvvm;
using TechFest.Models;
using Xamarin.Forms;

namespace TechFest.PageModels
{
    public class EventListPageModel : BasePageModel
    {
        public List<EventList> Events { get; set; }

        public EventListPageModel(IDataService dataService) :
            base(dataService)
        {
        }

        public Command<Event> EventSelected => new Command<Event>(HandleEventSelected);
		public Command RefreshCommand => new Command(async () => await LoadEvents());

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            if (!HasAppeared)
                await LoadEvents();

            base.ViewIsAppearing(sender, e);
        }

        private async Task LoadEvents()
        {
            IsBusy = true;

            try
            {
				Events = new List<EventList>();

				var currentEvents = new EventList("Current", await DataService.GetCurrentEventsAsync());
				var previous = new EventList("Past", await DataService.GetPreviousEventsAsync());

				Events = new List<EventList> { currentEvents, previous };
            }
            catch (Exception ex)
            {
				IsBusy = false;
                var msg = ex.Message;
                await CoreMethods.DisplayAlert("Whoops!", "There was a problem getting the events. Please try again " + msg, "Ok");
            }

            IsBusy = false;
        }

        private void HandleEventSelected(Event evnt)
        {
            if (evnt.Hosted.ToLower() == "yes")
            {
                DataService.SetBaseUrl(evnt.Url);

				if (CurrentNavigationServiceName != Constants.DefaultNavigationServiceName) {

					if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android) {
						CoreMethods.SwitchOutRootNavigation(NavigationContainerNames.MainContainer);
					} else {

                        MessagingCenter.Send(this, "Reload");

                        var masterDetail = FreshIOC.Container.Resolve<IFreshNavigationService>(NavigationContainerNames.MainContainer) as FreshMasterDetailNavigationContainer;
						var page = masterDetail.Detail;
					    page.Parent = null;
                        page.BackgroundColor = Color.Black;
						CurrentPage.Navigation.PushAsync(page);
					}  
				} 
            }
            else
            {
                Device.OpenUri(new Uri(evnt.Url));
            }
        }
    }

	public class EventList : List<Event>
	{
		public string Title { get; set; }

		public EventList(string title, List<Event> events)
		{
			Title = title;
			this.AddRange(events);
		}
	}
}