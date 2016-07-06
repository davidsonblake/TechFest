using System.Xml.Serialization;
using System.Collections.Generic;

namespace TechFest.Models
{

	[XmlRoot(ElementName = "field")]
	public class Session
	{
		[XmlElement(ElementName = "Track")]
		public string Track { get; set; }
		[XmlElement(ElementName = "Timeslot")]
		public string Timeslot { get; set; }
		[XmlElement(ElementName = "Day")]
		public string Day { get; set; }
		[XmlElement(ElementName = "Speakers")]
		public string Speakers { get; set; }
		[XmlElement(ElementName = "Description")]
		public string Description { get; set; }
		[XmlElement(ElementName = "Votes")]
		public string Votes { get; set; }

		public string Title { get; set;}
	}

}
