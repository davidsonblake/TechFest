using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TechFest.Models;
using Xamarin.Forms;

namespace TechFest.PageModels
{
	public class SessionListPageModel : BasePageModel
	{
	    public List<SessionList> Sessions { get; set; }

	    private Session _selectedSession;

		public Session SelectedSession {
			get {
				return _selectedSession;
			}
			set {
				_selectedSession = value;
				if (value != null)
					SessionSelected.Execute(value);
			}
		}


		public SessionListPageModel(IDataService dataService) 
			:base(dataService)
		{
		}

		public Command<Session> SessionSelected => new Command<Session>(HandleSessionSelected);

	    protected override async void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);

			try
			{
			    var sessions = (await DataService.GetSessionsAsync()).GroupBy(x => x.Track).Select(s => new {Key = s.Key, Sessions = s.ToList()}).ToList();
                var groupedSessions = new List<SessionList>();

			    foreach (var session in sessions)
			    {
			        groupedSessions.Add(new SessionList(session.Key, session.Sessions));
			    }

			    Sessions = groupedSessions;

			} catch (Exception ex) {
				await CoreMethods.DisplayAlert("Whoops!", ex.Message, "Ok");
			}
		}

		void HandleSessionSelected(Session session)
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