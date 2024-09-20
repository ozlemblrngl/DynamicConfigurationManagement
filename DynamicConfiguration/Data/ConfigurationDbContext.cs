﻿using Microsoft.EntityFrameworkCore;

namespace DynamicConfiguration.Data
{
	public class ConfigurationDbContext : DbContext
	{
		public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options) : base(options) { }

		public DbSet<Configuration> Configurations { get; set; }
	}
}
