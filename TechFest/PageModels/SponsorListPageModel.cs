using System;
using System.Collections.Generic;
using System.Linq;
using TechFest.Models;
using Xamarin.Forms;

namespace TechFest.PageModels
{
    public class SponsorListPageModel : BasePageModel
    {
        public List<SponsorList> Sponsors { get; set; }

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
				var sponsors = (await DataService.GetSponsersAsync()).GroupBy(x => x.SponsorshipLevel).Select(s => new { Key = s.Key, Sponsors = s.ToList() }).ToList();
				var groupedSponsors = new List<SponsorList>();

				foreach (var sponsor in sponsors) {
					groupedSponsors.Add(new SponsorList(sponsor.Key, sponsor.Sponsors));
				}

				Sponsors = groupedSponsors;
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