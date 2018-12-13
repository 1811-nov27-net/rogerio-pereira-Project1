using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.DataAccess
{
    public static class DbOptions
    {
        public static DbContextOptions<Project0Context> options = null;

        public static void ConfigDatabase()
        {
            var connectionString = SecretDatabaseAccess.SecretString;

            var optionsBuilder = new DbContextOptionsBuilder<Project0Context>();
            optionsBuilder.UseSqlServer(connectionString);
            options = optionsBuilder.Options;
        }

        public static Project0Context Context
        {
            get
            {
                return new Project0Context(options);
            }
        }
    }
}
