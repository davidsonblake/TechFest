﻿using System.Reflection;
using FreshMvvm;
using TechFest.PageModels;
using TechFest.Pages;
using TechFest.Services;
using Xamarin.Forms;
using XamSvg.XamForms;

namespace TechFest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

			var assembly = typeof(App).GetTypeInfo().Assembly;
			XamSvg.Shared.Config.ResourceAssembly = assembly;

            RegisterIoc();
            Akavache.BlobCache.ApplicationName = "TechFest";

			var mainMasterDetail = new FreshMasterDetailNavigationContainer(NavigationContainerNames.MainContainer);

			var mainTabbedNavigation = new FreshTabbedNavigationContainer();
            mainTabbedNavigation.BarBackgroundColor = Color.FromHex("#152129");
            mainTabbedNavigation.BarTextColor = Color.FromHex("#F05A79");
            mainTabbedNavigation.BackgroundColor = Color.FromHex("#152129");

            var speakers = mainTabbedNavigation.AddTab<SpeakerListPageModel>("Speakers", null) as NavigationPage;
            speakers.BackgroundColor = Color.Black;

			var sessions = mainTabbedNavigation.AddTab<SessionListPageModel>("Sessions", null);
            sessions.BackgroundColor = Color.Black;

            var sponsors = mainTabbedNavigation.AddTab<SponsorListPageModel>("Sponsors", null);
            sponsors.BackgroundColor = Color.Black;

			var page = FreshPageModelResolver.ResolvePageModel<EventListPageModel>();
			var eventPage = FreshPageModelResolver.ResolvePageModel<EventListPageModel>();

			mainMasterDetail.Master = eventPage;
			mainMasterDetail.Detail = mainTabbedNavigation;

            mainMasterDetail.BackgroundColor = Color.FromHex("#152129");

            var basicNavContainer = new FreshNavigationContainer(page, NavigationContainerNames.EventSelectionContainer);
            basicNavContainer.BarBackgroundColor = Color.FromHex("#152129");

			NavigationPage.SetHasNavigationBar(page, false);

            MainPage = basicNavContainer;

			MessagingCenter.Subscribe<EventListPageModel>(this, "Reload", (obj) => { if (mainMasterDetail.IsPresented) mainMasterDetail.IsPresented = false; });
        }

        private void RegisterIoc()
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