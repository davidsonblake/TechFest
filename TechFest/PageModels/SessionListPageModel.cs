using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechFest.Models;
using Xamarin.Forms;

namespace TechFest.PageModels
{
    public class SessionListPageModel : BasePageModel
    {
        public List<SessionList> Sessions { get; set; }

		public bool IsBgImageVisible => Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android;

		public SessionListPageModel(IDataService dataService)
			: base(dataService)
		{
			MessagingCenter.Subscribe<EventListPageModel>(this, "Reload", async (obj) => {
				await CoreMethods.PopToRoot(false);
				await LoadSessions();
			});
        }

        public Command<Session> SessionSelected => new Command<Session>(HandleSessionSelected);
		public Command RefreshCommand => new Command(async () => await LoadSessions(true));

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            if (!HasAppeared)
                await LoadSessions();

            base.ViewIsAppearing(sender, e);
        }

        private async Task LoadSessions(bool invalidate = false)
        {
            try
            {
				IsBusy = true;

				Sessions = new List<SessionList>();

                var sessions = (await DataService.GetSessionsAsync(invalidate)).GroupBy(x => x.Track).Select(s => new { Key = s.Key, Sessions = s.ToList() }).ToList();
                var groupedSessions = new List<SessionList>();

                foreach (var session in sessions)
                {
                    groupedSessions.Add(new SessionList(session.Key, session.Sessions));
                }

                Sessions = groupedSessions;
            }
            catch (Exception ex)
            {
				IsBusy = false;
                await CoreMethods.DisplayAlert("Whoops!", ex.Message, "Ok");
            }

			IsBusy = false;
        }

        private void HandleSessionSelected(Session session)
        {
            CoreMethods.PushPageModel<SessionPageModel>(session);
        }
    }

    public class SessionList : List<Session>
    {
        public string Title { get; set; }

        public SessionList(string title, List<Session> sessions)
        {
            Title = title;
            this.AddRange(sessions);
        }
    }
}