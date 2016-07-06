using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TechFest.Models
{
		[XmlRoot(ElementName = "field")]
		public class Track
		{
			[XmlElement(ElementName = "Room")]
			public string Room { get; set; }
			[XmlElement(ElementName = "SortOrder")]
			public string SortOrder { get; set; }
			[XmlElement(ElementName = "Active")]
			public string Active { get; set; }

			public string Title { get; set;}
		}

}
