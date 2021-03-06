﻿using Akavache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using TechFest.Models;

namespace TechFest.Services
{
    public class DataService : IDataService
    {
        private string _baseUrl;
        private readonly FeedParser _parser = new FeedParser();
		private string _originalBaseUrl;

		public string BaseUrl => _originalBaseUrl;

		private const int CacheMinutes = 15;

        public async Task<List<Event>> GetCurrentEventsAsync()
        {
            var url = "http://techfests.com/_layouts/powerrss.aspx?list=events&options=filter=current";

            return await BlobCache.LocalMachine.GetOrFetchObject(url, async () =>
            {
                var rssItems = (await _parser.Parse(new Uri(url, UriKind.Absolute))).ToList();
                var events = GetObjects<Event>(rssItems);

                for (int i = 0; i < events.Count(); i++)
                {
                    events[i].Title = rssItems[i].Title.Text;
					events[i].IsCurrent = true;
                }

                return events;
            }, DateTimeOffset.Now.AddDays(1));
        }

        public async Task<List<Event>> GetPreviousEventsAsync()
        {
            var url = "http://techfests.com/_layouts/powerrss.aspx?list=events&options=filter=past";

            return await BlobCache.LocalMachine.GetOrFetchObject(url, async () =>
            {
                var rssItems = (await _parser.Parse(new Uri(url, UriKind.Absolute))).ToList();
                var events = GetObjects<Event>(rssItems);

                for (int i = 0; i < events.Count(); i++)
                {
                    events[i].Title = rssItems[i].Title.Text;
					events[i].IsCurrent = false;
                }

                return events;
            }, DateTimeOffset.Now.AddDays(1));
        }

        public async Task<List<Session>> GetSessionsAsync(bool invalidate = false)
        {
            if (string.IsNullOrEmpty(_baseUrl))
                return default(List<Session>);

            var url = $"{_baseUrl}/_layouts/powerrss.aspx?list=Sessions";

			if (invalidate)
				await BlobCache.LocalMachine.InvalidateObject<List<Session>>(url);

            return await BlobCache.LocalMachine.GetOrFetchObject(url, async () =>
            {
                var rssItems = (await _parser.Parse(new Uri(url, UriKind.Absolute))).ToList();
                var sessions = GetObjects<Models.Session>(rssItems);

                for (int i = 0; i < sessions.Count(); i++)
                {
                    sessions[i].Title = rssItems[i].Title.Text;
                }

                return sessions;
            }, DateTimeOffset.Now.AddMinutes(CacheMinutes));
        }

        public async Task<List<Speaker>> GetSpeakersAsync(bool invalidate = false)
        {
            if (string.IsNullOrEmpty(_baseUrl))
                return default(List<Speaker>);

            var url = $"{_baseUrl}/_layouts/powerrss.aspx?list=Speakers";

			if (invalidate)
				await BlobCache.LocalMachine.InvalidateObject<List<Speaker>>(url);

            return await BlobCache.LocalMachine.GetOrFetchObject(url, async () =>
            {
                var rssItems = (await _parser.Parse(new Uri(url, UriKind.Absolute))).ToList();
                var speakers = GetObjects<Models.Speaker>(rssItems);

                for (int i = 0; i < speakers.Count(); i++)
                {
                    speakers[i].Name = rssItems[i].Title.Text;
					if (speakers[i].Link.StartsWith(@"//"))
						speakers[i].Link = "http:" + speakers[i].Link;
                }

                return speakers;
            }, DateTimeOffset.Now.AddMinutes(CacheMinutes));
        }

        public async Task<List<Sponsor>> GetSponsersAsync(bool invalidate = false)
        {
            if (string.IsNullOrEmpty(_baseUrl))
                return default(List<Sponsor>);

            var url = $"{_baseUrl}/_layouts/powerrss.aspx?list=Sponsors";

			if (invalidate)
				await BlobCache.LocalMachine.InvalidateObject<List<Sponsor>>(url);

            return await BlobCache.LocalMachine.GetOrFetchObject(url, async () =>
            {
                var rssItems = (await _parser.Parse(new Uri(url, UriKind.Absolute))).ToList();
                var sponsors = GetObjects<Sponsor>(rssItems);

                for (int i = 0; i < sponsors.Count(); i++)
                {
                    sponsors[i].Name = rssItems[i].Title.Text;
					if (sponsors[i].Link.StartsWith(@"//"))
						sponsors[i].Link = "http:" + sponsors[i].Link;
                }

                return sponsors;
            }, DateTimeOffset.Now.AddMinutes(CacheMinutes));
        }

        public async Task<List<Track>> GetTracksAsync(bool invalidate = false)
        {
            if (string.IsNullOrEmpty(_baseUrl))
                return default(List<Track>);

            var url = $"{_baseUrl}/_layouts/powerrss.aspx?list=Tracks";

			if (invalidate)
				await BlobCache.LocalMachine.InvalidateObject<List<Track>>(url);

            return await BlobCache.LocalMachine.GetOrFetchObject(url, async () =>
            {
                var rssItems = (await _parser.Parse(new Uri(url, UriKind.Absolute))).ToList();
                var tracks = GetObjects<Track>(rssItems);

                for (int i = 0; i < tracks.Count(); i++)
                {
                    tracks[i].Title = rssItems[i].Title.Text;
                }

                return tracks;
			}, DateTimeOffset.Now.AddMinutes(CacheMinutes));
        }

        public void SetBaseUrl(string baseUrl)
        {
			_originalBaseUrl = baseUrl;
			
            if (!string.IsNullOrEmpty(baseUrl) && baseUrl.EndsWith("/", StringComparison.CurrentCulture))
                baseUrl = baseUrl.Remove(baseUrl.LastIndexOf("/", StringComparison.CurrentCulture));

            _baseUrl = baseUrl;
        }

        private List<T> GetObjects<T>(IEnumerable<SyndicationItem> items)
        {
            var list = new List<T>();

            foreach (var item in items)
            {
                foreach (SyndicationElementExtension extension in item.ElementExtensions)
                {
                    if (extension.OuterName == "field")
                    {
                        var obj = extension.GetObject<XElement>();
                        list.Add(DeserializeExtension<T>(obj));
                    }
                }
            }

            return list;
        }

        private T DeserializeExtension<T>(XElement element)
        {
            Type type = typeof(T);
            StringReader reader = new StringReader(element.ToString());
            XmlSerializer xmlSerializer = new XmlSerializer(type);
            var obj = (T)xmlSerializer.Deserialize(reader);

            return obj;
        }
    }
}