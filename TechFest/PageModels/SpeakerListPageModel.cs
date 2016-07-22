using System;
using System.Collections.Generic;
using System.Linq;
using TechFest.Models;
using Xamarin.Forms;

namespace TechFest.PageModels
{
    public class SpeakerListPageModel : BasePageModel
    {
        public List<SpeakerPair> Speakers { get; set; }

        private Speaker _selectedSpeaker;

        public Speaker SelectedSpeaker
        {
            get
            {
                return _selectedSpeaker;
            }
            set
            {
                _selectedSpeaker = value;
                if (value != null)
                    SpeakerSelected.Execute(value);
            }
        }

        public SpeakerListPageModel(IDataService dataService)
            : base(dataService)
        {
        }

        public Command<Speaker> SpeakerSelected => new Command<Speaker>(HandleSpeakerSelected);

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            try
            {
                var speakers = await DataService.GetSpeakersAsync();
                var pairs = new List<SpeakerPair>();
                if (speakers != null)
                {
                    for (int i = 0; i < speakers.Count; i = i + 2)
                    {
                        var items = speakers.Skip(i).Take(2);
                        if (items != null)
                        {
                            var pair = new SpeakerPair();
                            pair.Speaker1 = items.First();
                            if (items.Count() > 1)
                                pair.Speaker2 = items.Skip(1).Take(1).First();

                            pairs.Add(pair);
                        }
                    }

                    Speakers = pairs;

                }
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Whoops!", ex.Message, "Ok");
            }
        }

        private void HandleSpeakerSelected(Speaker speaker)
        {
            CoreMethods.PushPageModel<SpeakerPageModel>(speaker);
        }
    }
}