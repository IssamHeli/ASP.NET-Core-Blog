using System;
using Microsoft.EntityFrameworkCore;
using Technexa.Models;

namespace Technexa.Data
{
	public class DBContextApplication : DbContext
	{

        public DBContextApplication(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Technexa.Models.Categorie> Categorie { get; set; } = default!;


        public DbSet<admin> admins { get; set; }


        public DbSet<Message> Messages { get; set; }
	}
}

