using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TechFest.Models;
using Xamarin.Forms;

namespace TechFest.PageModels
{
	public class CurrentEventsPageModel : BasePageModel 
	{
		public List<Event> Events { get; set; }

		Event _selectedEvent;

		public Event SelectedEvent {
			get {
				return _selectedEvent;
			}
			set {
				_selectedEvent = value;
				if (value != null)
					EventSelected.Execute(value);
			}
		}

		public CurrentEventsPageModel(IDataService dataService)
			:base(dataService)
		{
		}

		public Command<Event> EventSelected => new Command<Event>(HandleEventSelected);

	    protected override async void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);

			IsBusy = true;

			try {
				var events = await DataService.GetCurrentEventsAsync();
			    Events = events;
			} catch (Exception ex) {
				var msg = ex.Message;
				await CoreMethods.DisplayAlert("Whoops!", "There was a problem getting the events. Please try again " + msg, "Ok");
			}

			IsBusy = false;
		}

		private void HandleEventSelected(Event evnt)
		{
			if (evnt.Hosted.ToLower() == "yes") {
				DataService.SetBaseUrl(evnt.Url);

				CoreMethods.SwitchOutRootNavigation(NavigationContainerNames.MainContainer);
			} else {
				Device.OpenUri(new Uri(evnt.Url));
			}

		}
	}
}

