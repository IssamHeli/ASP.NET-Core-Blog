using System;
using System.ComponentModel.DataAnnotations;

namespace Technexa.Models
{
	public class Post
	{
		[Key]
		public int idpost { get; set; }
		public required string Title { get; set; }
		public required string Description { get; set; }
		public required string SrcYoutubeVedio { get; set; }
        public required string Srcimage { get; set; }
        public DateTime DateCreated { get; set; }


        public string categorie { get; set; }
    }
}

