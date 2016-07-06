using System;
using FreshMvvm;
using TechFest.PageModels;
using TechFest.Services;
using Xamarin.Forms;

namespace TechFest
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			RegisterIoc();
            Akavache.BlobCache.ApplicationName = "TechFest";

            var mainTabbedNavigation = new FreshTabbedNavigationContainer(NavigationContainerNames.MainContainer);
			mainTabbedNavigation.AddTab<SpeakerListPageModel>("Speakers", null);
			mainTabbedNavigation.AddTab<SessionListPageModel>("Sessions", null);
			mainTabbedNavigation.AddTab<SponsorListPageModel>("Sponsors", null);
		    mainTabbedNavigation.AddTab<FeedPageModel>("Feed", null);

			var eventsTabbedNavigation = new FreshTabbedNavigationContainer(NavigationContainerNames.EventSelectionContainer);
			eventsTabbedNavigation.AddTab<CurrentEventsPageModel>("Current", null);
			eventsTabbedNavigation.AddTab<PastEventsPageModel>("Past", null);

			MainPage = eventsTabbedNavigation;
		    
		}

		void RegisterIoc()
		{
			FreshIOC.Container.Register<IDataService>(new DataService());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

