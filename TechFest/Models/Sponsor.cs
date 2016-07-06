using System.Xml.Serialization;
using System.Collections.Generic;

namespace TechFest.Models
{

	[XmlRoot(ElementName = "field")]
	public class Sponsor
	{
		[XmlElement(ElementName = "Link")]
		public string Link { get; set; }
		[XmlElement(ElementName = "SponsorshipLevel")]
		public string SponsorshipLevel { get; set; }
		[XmlElement(ElementName = "SortOrder")]
		public string SortOrder { get; set; }
		[XmlElement(ElementName = "Active")]
		public string Active { get; set; }
		[XmlElement(ElementName = "Alt")]
		public string Alt { get; set; }
		[XmlElement(ElementName = "Description")]
		public string Description { get; set; }
		[XmlElement(ElementName = "Instagram")]
		public string Instagram { get; set; }
		[XmlElement(ElementName = "Facebook")]
		public string Facebook { get; set; }
		[XmlElement(ElementName = "LinkedIn")]
		public string LinkedIn { get; set; }
		[XmlElement(ElementName = "Twitter")]
		public string Twitter { get; set; }
		[XmlElement(ElementName = "YouTube")]
		public string YouTube { get; set; }
		[XmlElement(ElementName = "Rss")]
		public string Rss { get; set; }
		[XmlElement(ElementName = "GooglePlus")]
		public string GooglePlus { get; set; }
		[XmlElement(ElementName = "Flickr")]
		public string Flickr { get; set; }
		[XmlElement(ElementName = "Github")]
		public string Github { get; set; }
		[XmlElement(ElementName = "Pinterest")]
		public string Pinterest { get; set; }
		[XmlElement(ElementName = "StackOverflow")]
		public string StackOverflow { get; set; }

		public string Name { get; set;}
	}

}
