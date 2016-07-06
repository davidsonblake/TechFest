using System;
using System.Collections.Generic;
using System.Linq;
using TechFest.Models;
using Xamarin.Forms;

namespace TechFest.PageModels
{
	public class SpeakerListPageModel : BasePageModel
	{
		public List<Speaker> Speakers { get; set; }

		private Speaker _selectedSpeaker;

		public Speaker SelectedSpeaker {
			get {
				return _selectedSpeaker;
			}
			set {
				_selectedSpeaker = value;
				if (value != null)
					SpeakerSelected.Execute(value);
			}
		}

		public SpeakerListPageModel(IDataService dataService)
			:base(dataService)
		{
		}

		public Command<Speaker> SpeakerSelected => new Command<Speaker>(HandleSpeakerSelected);

	    protected override async void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);

			try {
				var speakers = await DataService.GetSpeakersAsync();
				if (speakers != null)
					Speakers = speakers.OrderBy(x => x.SortOrder).ToList();
			} catch (Exception ex) {
				await CoreMethods.DisplayAlert("Whoops!", ex.Message, "Ok");
			}
		}

		private void HandleSpeakerSelected(Speaker speaker)
		{
		    CoreMethods.PushPageModel<SpeakerPageModel>(speaker);
		}
	}
}

