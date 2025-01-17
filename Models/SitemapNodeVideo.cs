using System;
namespace Technexa.Models
{
	public class SitemapNodeVideo
	{
        public string Location { get; set; }
        public DateTime LastModified { get; set; }
        public SitemapChangeFrequency ChangeFrequency { get; set; }
        public double Priority { get; set; }

        // Video-specific properties
        public string VideoTitle { get; set; }
        public string VideoDescription { get; set; }
        public string VideoThumbnailUrl { get; set; }

        // New video-specific properties for Google Search Console
        public string VideoContentLoc { get; set; }
    }

    public enum SitemapChangeFrequencyvedio
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

