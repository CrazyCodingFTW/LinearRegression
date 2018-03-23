using LinearRegression.Database.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database
{
    internal class LinearRegressionDbContext : DbContext
    {
        internal DbSet<Analysis> AnalysisSet { get; set; }

        public LinearRegressionDbContext()
        {
            SQLitePCL.Batteries.Init();
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AnalysisDatabase.sqlite");
        }
    }
}
