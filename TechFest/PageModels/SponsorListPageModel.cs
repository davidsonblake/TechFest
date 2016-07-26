using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechFest.Models;
using Xamarin.Forms;

namespace TechFest.PageModels
{
    public class SponsorListPageModel : BasePageModel
    {
        public List<SponsorList> Sponsors { get; set; }

		public bool IsBgImageVisible => Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android;

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
		public Command RefreshCommand => new Command(async () => await LoadSponsors(true));

        public SponsorListPageModel(IDataService dataService)
            : base(dataService)
        {
			MessagingCenter.Subscribe<EventListPageModel>(this, "Reload", async (obj) => {
                if(!HasAppeared)
                    return;

				await CoreMethods.PopToRoot(false);
				await LoadSponsors();
			});
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            if (!HasAppeared)
                await LoadSponsors();

            base.ViewIsAppearing(sender, e);
        }

        private async Task LoadSponsors(bool invalidate = false)
        {
            try
            {
				IsBusy = true;
				
				Sponsors = new List<SponsorList>();

                var sponsors = (await DataService.GetSponsersAsync(invalidate)).GroupBy(x => x.SponsorshipLevel).Select(s => new { Key = s.Key, Sponsors = s.ToList() }).ToList();
                var groupedSponsors = new List<SponsorList>();

                foreach (var sponsor in sponsors)
                {
                    groupedSponsors.Add(new SponsorList(sponsor.Key, sponsor.Sponsors));
                }

                Sponsors = groupedSponsors;
            }
            catch (Exception ex)
            {
				IsBusy = false;
                await CoreMethods.DisplayAlert("Whoops!", ex.Message, "Ok");
            }

			IsBusy = false;
        }

        private void HandleSponsorSelected(Sponsor sponsor)
        {
            CoreMethods.PushPageModel<SponsorPageModel>(sponsor);
        }
    }

    public class SponsorList : List<Sponsor>
    {
        public string Title { get; set; }

        public SponsorList(string title, List<Sponsor> sponsors)
        {
            Title = title;
            this.AddRange(sponsors);
        }
    }
}