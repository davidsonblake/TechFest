using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFest.Models;
using Xamarin.Forms;

namespace TechFest.PageModels
{
    public class EventListPageModel : BasePageModel
    {
        public List<Event> Events { get; set; }

        private Event _selectedEvent;

        public Event SelectedEvent
        {
            get
            {
                return _selectedEvent;
            }
            set
            {
                _selectedEvent = value;
                if (value != null)
                    EventSelected.Execute(value);
            }
        }

        public EventListPageModel(IDataService dataService) :
            base(dataService)
        {
        }

        public Command<Event> EventSelected => new Command<Event>(HandleEventSelected);

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
                var events = await DataService.GetCurrentEventsAsync();
                events.AddRange(await DataService.GetPreviousEventsAsync());
                Events = events;
            }
            catch (Exception ex)
            {
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

                CoreMethods.SwitchOutRootNavigation(NavigationContainerNames.MainContainer);
            }
            else
            {
                Device.OpenUri(new Uri(evnt.Url));
            }
        }
    }
}