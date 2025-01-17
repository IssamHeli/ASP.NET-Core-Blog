using System;
using System.ComponentModel.DataAnnotations;

namespace Technexa.Models
{
	public class admin
	{
		[Key]
		public int id { get; set; }
		public required string Username { get; set; }
		public required string keyenter { get; set; }
	}
}

