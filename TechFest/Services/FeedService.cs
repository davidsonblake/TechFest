using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TechFest.Services
{
    public class FeedParser
    {
        private static readonly Regex HtmlRegex = new Regex(@"<[^>]*>", RegexOptions.CultureInvariant);    //@"<(.|\n)*?>"
        private static readonly Regex ControlCodesRegex = new Regex(@"[\x00-\x1F\x7f]", RegexOptions.CultureInvariant);
        private static readonly Regex WhiteSpaceRegex = new Regex(@"\s{2,}", RegexOptions.CultureInvariant);

        public async Task<IEnumerable<SyndicationItem>> Parse(Uri url)
        {
            using (var httpClient = new HttpClient())
            {
                var content = await httpClient.GetStringAsync(url).ConfigureAwait(false);
                return Parse(content);
            }
        }

        public IEnumerable<SyndicationItem> Parse(string content)
        {
            var feed = SyndicationFeed.Load(XmlReader.Create(new StringReader(content)));
            return feed.Items;
        }

        private static readonly Func<XElement, string, XElement> Find =
            (item, name) => item.Elements().FirstOrDefault(i => i.Name.LocalName.Equals(name));

        /// <summary>
        /// Try to parse a Date.
        /// </summary>
        /// <param name="date">The string to parse to a DateTime</param>
        /// <returns>The date in a DateTime format or the minimum value of DateTime in case of error.</returns>
        private static DateTime ParseDate(string date)
        {
            DateTime result;
            return DateTime.TryParse(date, out result) ? result : DateTime.MinValue;
        }

        private static string Normalize(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = HtmlDecode(value);
                if (string.IsNullOrEmpty(value))
                    return value;

                value = StripHTML(value);
                value = StripDoubleOrMoreWhiteSpace(RemoveControlChars(value));
                //value = value.Normalize().Trim();
            }
            return value;
        }

        private static string RemoveControlChars(string value)
        {
            return ControlCodesRegex.Replace(value, " ");
        }

        private static string StripDoubleOrMoreWhiteSpace(string value)
        {
            return WhiteSpaceRegex.Replace(value, " ");
        }

        private static string StripHTML(string value)
        {
            return HtmlRegex.Replace(value, " ");
        }

        private static string HtmlDecode(string value, int threshold = 5)
        {
            int c = 0;
            string newvalue = WebUtility.HtmlDecode(value);
            while (!newvalue.Equals(value) && c < threshold)    //Keep decoding (if a string is double/triple/... encoded; we want the original)
            {
                c++;
                value = newvalue;
                newvalue = WebUtility.HtmlDecode(value);
            }
            if (c >= threshold) //Decoding threshold exceeded?
                return null;

            return newvalue;
        }
    }
}