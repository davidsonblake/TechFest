using System;
using System.Collections.Generic;
using TechFest.Models;
using Xamarin.Forms;

namespace TechFest.PageModels
{
    public class SponsorListPageModel : BasePageModel
    {
        public List<Sponsor> Sponsors { get; set; }

        private Sponsor _selectedSponsor;

        public Sponsor SelectedSponsor
        {
            get
            {
                return _selectedSponsor;
            }
            set
            {
                _selectedSponsor = value;
                if (value != null)
                    SponsorSelected.Execute(value);
            }
        }

        public Command<Sponsor> SponsorSelected => new Command<Sponsor>(HandleSponsorSelected);

        public SponsorListPageModel(IDataService dataService)
            : base(dataService)
        {
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            try
            {
                Sponsors = await DataService.GetSponsersAsync();
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Whoops!", ex.Message, "Ok");
            }
        }

        private void HandleSponsorSelected(Sponsor sponsor)
        {
            CoreMethods.PushPageModel<SponsorPageModel>(sponsor);
        }
    }
}