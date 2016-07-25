using System.Collections.Generic;
using System.Threading.Tasks;
using TechFest.Models;

namespace TechFest
{
    public interface IDataService
    {
		string BaseUrl { get; }
		
        void SetBaseUrl(string baseUrl);

        Task<List<Event>> GetCurrentEventsAsync();

        Task<List<Event>> GetPreviousEventsAsync();

        Task<List<Session>> GetSessionsAsync(bool invalidate = false);

        Task<List<Speaker>> GetSpeakersAsync(bool invalidate = false);

        Task<List<Sponsor>> GetSponsersAsync(bool invalidate = false);

        Task<List<Track>> GetTracksAsync(bool invalidate = false);
    }
}