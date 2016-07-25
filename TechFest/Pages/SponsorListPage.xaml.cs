﻿using TechFest.PageModels;
using Xamarin.Forms;

namespace TechFest.Pages
{
    public partial class SponsorListPage : ContentPage
    {
        public SponsorListPage()
        {
            InitializeComponent();
        }

		void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;

			lstSponsors.SelectedItem = null;
			((SponsorListPageModel)this.BindingContext).SponsorSelected.Execute(e.SelectedItem);
		}
	}
}