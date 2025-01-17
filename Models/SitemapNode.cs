using System;
namespace Technexa.Models
{
	public class SitemapNode
	{
        public string Location { get; set; }
        public DateTime LastModified { get; set; }
        public SitemapChangeFrequency ChangeFrequency { get; set; }
        public double Priority { get; set; }

    }

    public enum SitemapChangeFrequency
    {
        Always,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly,
        Never
    }
}

