﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechFest.Models;

namespace TechFest.PageModels
{
    public class SpeakerListPageModel : BasePageModel
    {
        public List<SpeakerPair> Speakers { get; set; }

        public SpeakerListPageModel(IDataService dataService)
            : base(dataService)
        {
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            if (!HasAppeared)
                await LoadSpeakers();

            base.ViewIsAppearing(sender, e);
        }

        private async Task LoadSpeakers()
        {
            try
            {
                IsBusy = true;

                var speakers = await DataService.GetSpeakersAsync();
                var pairs = new List<SpeakerPair>();
                if (speakers != null)
                {
                    for (int i = 0; i < speakers.Count; i = i + 2)
                    {
                        var items = speakers.Skip(i).Take(2);
                        if (items != null)
                        {
                            var pair = new SpeakerPair(HandleSpeakerSelected);
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
                IsBusy = false;
                await CoreMethods.DisplayAlert("Whoops!", ex.Message, "Ok");
            }
            IsBusy = false;
        }

        private void HandleSpeakerSelected(Speaker speaker)
        {
            CoreMethods.PushPageModel<SpeakerPageModel>(speaker);
        }
    }
}