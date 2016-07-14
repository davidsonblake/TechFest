using System.Collections.Generic;
using System.Threading.Tasks;
using TechFest.Models;

namespace TechFest
{
    public interface IDataService
    {
        void SetBaseUrl(string baseUrl);

        Task<List<Event>> GetCurrentEventsAsync();

        Task<List<Event>> GetPreviousEventsAsync();

        Task<List<Session>> GetSessionsAsync();

        Task<List<Speaker>> GetSpeakersAsync();

        Task<List<Sponsor>> GetSponsersAsync();

        Task<List<Track>> GetTracksAsync();
    }
}