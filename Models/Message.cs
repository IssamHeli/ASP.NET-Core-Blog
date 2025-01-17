using System;
using System.ComponentModel.DataAnnotations;

namespace Technexa.Models
{
	public class Message
	{
		[Key]
		public int id { get; set; }

		public required string name { get; set; }

		public required string email { get; set; }

        public required string message { get; set; }
	}
}

